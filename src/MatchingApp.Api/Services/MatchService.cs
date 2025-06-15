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

        public CompatibilityResult CalculateCompatibilityDetail(NatalChart? a, NatalChart? b)
        {
            var result = new CompatibilityResult();
            if (a == null || b == null)
            {
                result.Reasons.Add("Missing natal chart information");
                return result;
            }

            if (!string.IsNullOrEmpty(a.SunSign) && a.SunSign == b.SunSign)
            {
                result.Score += 4;
                result.Reasons.Add($"Both share Sun sign {a.SunSign}");
            }
            if (!string.IsNullOrEmpty(a.MoonSign) && a.MoonSign == b.MoonSign)
            {
                result.Score += 3;
                result.Reasons.Add($"Both share Moon sign {a.MoonSign}");
            }
            if (!string.IsNullOrEmpty(a.Ascendant) && a.Ascendant == b.Ascendant)
            {
                result.Score += 3;
                result.Reasons.Add($"Both share Ascendant {a.Ascendant}");
            }

            return result;
        }
    }
}
