namespace MatchingApp.Api.Models
{
    public class CompatibilityResult
    {
        public double Score { get; set; }
        public double StarRating { get; set; }
        public List<string> Reasons { get; } = new();
        public List<SignCompatibility> Breakdown { get; } = new();
    }
}
