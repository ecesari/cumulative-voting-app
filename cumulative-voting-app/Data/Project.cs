using System.Collections.Generic;

namespace cumulative_voting_app.Data
{
    public class Project
    {

        public Project(string hash, List<string> requirements) {
            Hash = hash;
            Votes = new List<Vote>();
            Requirements = requirements;
        }

        public string Hash { get; set; }

        public List<Vote> Votes { get; set; }

        public List<string> Requirements { get; set; }
    }
}
