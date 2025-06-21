namespace MatchingApp.Api.Models
{
    public class SignCompatibility
    {
        public string Planet { get; set; } = string.Empty;
        public string? SignA { get; set; }
        public string? SignB { get; set; }
        public bool Match { get; set; }
        public string? Aspect { get; set; }
        public string? AspectSymbol { get; set; } // "□", "△", etc.
        public string? AspectDescription { get; set; } // "Power struggles, loyalty...", etc.
    }
}
