using MatchingApp.Api.Models;

namespace MatchingApp.Api.Services
{
    public class MatchService
    {
        public double CalculateCompatibility(NatalChart? a, NatalChart? b)
        {
            return CalculateCompatibilityDetail(a, b).Score;
        }

        public CompatibilityResult CalculateCompatibilityDetail(NatalChart? a, NatalChart? b)
        {
            var result = new CompatibilityResult();
            if (a == null || b == null)
            {
                result.Reasons.Add("Missing natal chart information");
                return result;
            }

            CompareSign(result, a.SunSign, b.SunSign, "Sun");
            CompareSign(result, a.MoonSign, b.MoonSign, "Moon");
            CompareSign(result, a.MercurySign, b.MercurySign, "Mercury");
            CompareSign(result, a.VenusSign, b.VenusSign, "Venus");
            CompareSign(result, a.MarsSign, b.MarsSign, "Mars");
            CompareSign(result, a.JupiterSign, b.JupiterSign, "Jupiter");
            CompareSign(result, a.SaturnSign, b.SaturnSign, "Saturn");
            CompareSign(result, a.UranusSign, b.UranusSign, "Uranus");
            CompareSign(result, a.NeptuneSign, b.NeptuneSign, "Neptune");
            CompareSign(result, a.PlutoSign, b.PlutoSign, "Pluto");
            CompareSign(result, a.Ascendant, b.Ascendant, "Ascendant");

            // Scale to 0-100 percentage (11 possible matches)
            result.Score = result.Score / 11.0 * 100.0;
            result.StarRating = Math.Round(result.Score / 20.0, 1);

            return result;
        }

        private void CompareSign(CompatibilityResult result, string? a, string? b, string label)
        {
            var aspect = ZodiacHelper.GetAspect(a, b);
            double points = aspect switch
            {
                "Conjunction" => 1.0,
                "Trine" or "Sextile" => 0.8,
                "Square" or "Opposition" => 0.4,
                _ => 0.2
            };

            var (aspectSymbol, aspectDesc) = AspectDetailsHelper.GetAspectInfo(aspect, label, a, b);

            result.Breakdown.Add(new SignCompatibility
            {
                Planet = label,
                SignA = a,
                SignB = b,
                Match = aspect == "Conjunction",
                Aspect = aspect,
                AspectSymbol = aspectSymbol,
                AspectDescription = aspectDesc
            });

            result.Score += points;

            // More informative reason for the API caller
            result.Reasons.Add($"{label}: {a} vs {b} {aspectSymbol} {aspect} – {aspectDesc}");
        }


    }
}
