using System;
using System.Collections.Generic;
using System.Linq;

namespace cumulative_voting_app.Data
{
    public class VotingService
    {

        private Dictionary<string, Project> projectStorage = new Dictionary<string, Project>();

        public string CreateProject(List<string> requirements)
        {
            if (requirements.Count == 0)
            {
                throw new System.ArgumentException("Must have requirements.");
            }
            var isRequirementsUnique = (new HashSet<string>(requirements)).Count == requirements.Count;
            if (!isRequirementsUnique)
            {
                throw new System.ArgumentException("All requirements must be unique.");
            }
            var hash = Guid.NewGuid().ToString().Replace("-", "");
            var project = new Project(hash, requirements);
            projectStorage[hash] = project;
            return hash;
        }

        public List<string> GetProjectRequirements(string projectHash)
        {
            return projectStorage[projectHash].Requirements;
        }

        public void AddVotes(List<Vote> votes, string projectHash)
        {
            var project = projectStorage[projectHash];
            var hasAllRequirements = votes.Count == project.Requirements.Count && votes.TrueForAll(v => project.Requirements.Contains(v.RequirementName));
            if (!hasAllRequirements)
            {
                throw new System.ArgumentException("There has to be a vote for each requirement.");
            }

            var votesTotalTo100 = votes.Aggregate(0, (acc, v) => acc + v.Points) == 100;
            if (!votesTotalTo100)
            {
                throw new System.ArgumentException("Sum of all votes should be 100.");
            }

            var allSameUser = (new HashSet<string>(votes.Select(v => v.StakeholderName))).Count == 1;
            if (!allSameUser)
            {
                throw new System.ArgumentException("All stakeholder name fields should be identical.");
            }

            var votedBefore = project.Votes.Exists(v => v.StakeholderName == votes.ElementAt(0).StakeholderName);
            if (votedBefore)
            {
                throw new System.ArgumentException("This user has voted before.");
            }

            project.Votes.AddRange(votes);
            projectStorage[projectHash] = project;
        }

        public IList<VoteResults> GetResults(string projectHash)
        {
            var project = projectStorage[projectHash];
            //var votesPerStakeholder = project.Votes
            //    .GroupBy(vote => vote.StakeholderName)
            //    .ToDictionary(g => g.Key, g => g.ToList());

            //var totalVotesPerRequirement = project.Votes
            //    .GroupBy(vote => vote.RequirementName)
            //    .ToDictionary(g => g.Key, g => g.Aggregate(0, (acc, v) => acc + v.Points));

            //var result = new VoteResults(totalVotesPerRequirement, votesPerStakeholder);
            var list = new List<VoteResults>();
            foreach (var projectRequirement in project.Requirements)
            {
                var result = new VoteResults {Requirement = projectRequirement, Votes = new List<Vote>()};
                foreach (var vote in project.Votes.Where(x => x.RequirementName == projectRequirement))
                {
                    result.Votes.Add(new Vote(vote.StakeholderName, projectRequirement, vote.Points));
                }
                list.Add(result);
            }
            return list;
        }
    }
}