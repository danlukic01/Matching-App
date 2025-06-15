namespace MatchingApp.Api.Models
{
    public class MatchRecommendation
    {
        public Client? Client { get; set; }
        public double Score { get; set; }
        public List<string> Reasons { get; set; } = new();
    }
}
