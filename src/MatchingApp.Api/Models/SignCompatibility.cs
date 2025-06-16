namespace MatchingApp.Api.Models
{
    public class SignCompatibility
    {
        public string Planet { get; set; } = string.Empty;
        public string? SignA { get; set; }
        public string? SignB { get; set; }
        public bool Match { get; set; }
    }
}
