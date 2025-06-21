namespace MatchingApp.Api.Services
{
    public static class AspectDetailsHelper
    {
        public static (string symbol, string description) GetAspectInfo(string aspect, string planet, string? a, string? b)
        {
            return aspect switch
            {
                "Conjunction" => ("✓", $"{planet} in the same sign ({a}); strong alignment."),
                "Trine" => ("△", $"{planet}: {a} vs {b} (Trine; natural harmony, flow)."),
                "Sextile" => ("⚹", $"{planet}: {a} vs {b} (Sextile; easy, supportive)."),
                "Square" => ("□", $"{planet}: {a} vs {b} (Square; growth through tension or challenge)."),
                "Opposition" => ("☍", $"{planet}: {a} vs {b} (Opposition; polarity, complementary or tension)."),
                "Quincunx" => ("–", $"{planet}: {a} vs {b} (Quincunx; awkward, adjustment needed)."),
                _ => ("–", $"{planet}: {a} vs {b} (No major aspect)."),
            };
        }
    }

}
