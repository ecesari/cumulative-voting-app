namespace cumulative_voting_app.Data
{
    public class Vote
    {
        public Vote(string stakeholderName, string requirementName, int points)
        {
            StakeholderName = stakeholderName;
            RequirementName = requirementName;
            Points = points;
        }

        public string StakeholderName { get; set; }

        public string RequirementName { get; set; }

        public int Points { get; set; }
    }
}
