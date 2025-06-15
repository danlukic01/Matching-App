namespace MatchingApp.Api.Models
{
    public class CompatibilityResult
    {
        public double Score { get; set; }
        public List<string> Reasons { get; } = new();
    }
}
