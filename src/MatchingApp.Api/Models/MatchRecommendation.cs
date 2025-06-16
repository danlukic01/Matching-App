namespace MatchingApp.Api.Models
{
    public class MatchRecommendation
    {
        public Client? Client { get; set; }
        public double Score { get; set; }
        public double StarRating { get; set; }
        public List<string> Reasons { get; set; } = new();
        public List<SignCompatibility> Breakdown { get; set; } = new();
    }
}
