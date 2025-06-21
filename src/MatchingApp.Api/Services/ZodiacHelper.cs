namespace MatchingApp.Api.Services
{
    public static class ZodiacHelper
    {
        // Signs as numbers: Aries=0, Taurus=1, ..., Pisces=11
        private static readonly Dictionary<string, int> SignIndices = new()
    {
        {"Aries", 0}, {"Taurus", 1}, {"Gemini", 2}, {"Cancer", 3},
        {"Leo", 4}, {"Virgo", 5}, {"Libra", 6}, {"Scorpio", 7},
        {"Sagittarius", 8}, {"Capricorn", 9}, {"Aquarius", 10}, {"Pisces", 11}
    };

        public static int GetSignIndex(string? sign)
            => sign != null && SignIndices.ContainsKey(sign) ? SignIndices[sign] : -1;

        public static string GetAspect(string? a, string? b)
        {
            int iA = GetSignIndex(a), iB = GetSignIndex(b);
            if (iA == -1 || iB == -1) return "None";
            int diff = Math.Abs(iA - iB);
            if (diff > 6) diff = 12 - diff; // for wrapping (e.g. Pisces vs Aries)
            return diff switch
            {
                0 => "Conjunction",
                1 or 11 => "Semisextile",
                2 or 10 => "Sextile",
                3 or 9 => "Square",
                4 or 8 => "Trine",
                5 or 7 => "Quincunx",
                6 => "Opposition",
                _ => "None"
            };
        }
    }

}
