using MatchingApp.Api.Models;

namespace MatchingApp.Api.Services
{
    public class NatalChartService
    {
        private static readonly string[] Signs = new[]
        {
            "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo",
            "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces"
        };

        public NatalChart Calculate(Client client)
        {
            var sun = GetSunSign(client.BirthDate);
            var moon = GetSignFromOffset(client.BirthDate.DayOfYear + client.BirthTime.Hours);
            var asc = GetSignFromOffset((int)client.BirthTime.TotalMinutes + (client.BirthLocation?.Length ?? 0));

            return new NatalChart
            {
                SunSign = sun,
                MoonSign = moon,
                Ascendant = asc
            };
        }

        private string GetSunSign(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;

            return (month, day) switch
            {
                (3, >=21) or (4, <=19) => "Aries",
                (4, >=20) or (5, <=20) => "Taurus",
                (5, >=21) or (6, <=20) => "Gemini",
                (6, >=21) or (7, <=22) => "Cancer",
                (7, >=23) or (8, <=22) => "Leo",
                (8, >=23) or (9, <=22) => "Virgo",
                (9, >=23) or (10, <=22) => "Libra",
                (10, >=23) or (11, <=21) => "Scorpio",
                (11, >=22) or (12, <=21) => "Sagittarius",
                (12, >=22) or (1, <=19) => "Capricorn",
                (1, >=20) or (2, <=18) => "Aquarius",
                _ => "Pisces"
            };
        }

        private string GetSignFromOffset(int offset)
        {
            int index = Math.Abs(offset) % Signs.Length;
            return Signs[index];
        }
    }
}
