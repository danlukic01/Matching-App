using MatchingApp.Api.Models;

namespace MatchingApp.Api.Services
{
    public class MatchService
    {
        public double CalculateCompatibility(NatalChart? a, NatalChart? b)
        {
            if (a == null || b == null)
            {
                return 0.0;
            }

            double score = 0.0;
            if (!string.IsNullOrEmpty(a.SunSign) && a.SunSign == b.SunSign)
            {
                score += 40;
            }
            if (!string.IsNullOrEmpty(a.MoonSign) && a.MoonSign == b.MoonSign)
            {
                score += 30;
            }
            if (!string.IsNullOrEmpty(a.Ascendant) && a.Ascendant == b.Ascendant)
            {
                score += 30;
            }

            return score;
        }
    }
}
