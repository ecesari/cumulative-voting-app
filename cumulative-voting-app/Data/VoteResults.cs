using System.Collections.Generic;

namespace cumulative_voting_app.Data
{
    public class VoteResults
    {
        //public Dictionary<string, int> TotalPointsForRequirement { get; set; }

        //public Dictionary<string, List<Vote>> VotesPerStakeholder;

        //public VoteResults(Dictionary<string, int> totalPointsForRequirement, Dictionary<string, List<Vote>> votesPerStakeholder)
        //{
        //    TotalPointsForRequirement = totalPointsForRequirement;
        //    VotesPerStakeholder = votesPerStakeholder;
        //}

        public string Requirement { get; set; }
        public IList<Vote> Votes { get; set; }
    }
}
