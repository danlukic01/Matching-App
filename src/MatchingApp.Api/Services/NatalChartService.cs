using MatchingApp.Api.Models;
using SwissEphNet;

namespace MatchingApp.Api.Services
{
    public class NatalChartService
    {
        private static readonly string[] Signs = new[]
        {
            "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo",
            "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces"
        };

        private readonly SwissEph _swe;

        public NatalChartService()
        {
            _swe = new SwissEph();
            _swe.swe_set_ephe_path(null);
        }

        public NatalChart Calculate(Client client)
        {
            double hour = client.BirthTime.TotalHours;
            DateTime d = client.BirthDate;
            double jd = _swe.swe_julday(d.Year, d.Month, d.Day, hour, SwissEph.SE_GREG_CAL);
            int flag = SwissEph.SEFLG_SWIEPH;

            var chart = new NatalChart
            {
                SunSign = GetSign(CalcPlanet(jd, SwissEph.SE_SUN, flag)),
                MoonSign = GetSign(CalcPlanet(jd, SwissEph.SE_MOON, flag)),
                MercurySign = GetSign(CalcPlanet(jd, SwissEph.SE_MERCURY, flag)),
                VenusSign = GetSign(CalcPlanet(jd, SwissEph.SE_VENUS, flag)),
                MarsSign = GetSign(CalcPlanet(jd, SwissEph.SE_MARS, flag)),
                JupiterSign = GetSign(CalcPlanet(jd, SwissEph.SE_JUPITER, flag)),
                SaturnSign = GetSign(CalcPlanet(jd, SwissEph.SE_SATURN, flag)),
                UranusSign = GetSign(CalcPlanet(jd, SwissEph.SE_URANUS, flag)),
                NeptuneSign = GetSign(CalcPlanet(jd, SwissEph.SE_NEPTUNE, flag)),
                PlutoSign = GetSign(CalcPlanet(jd, SwissEph.SE_PLUTO, flag)),
                Ascendant = GetAscendant(jd)
            };

            return chart;
        }

        private double CalcPlanet(double jd, int planet, int flags)
        {
            double[] xx = new double[6];
            string serr = string.Empty;
            _swe.swe_calc_ut(jd, planet, flags, xx, ref serr);
            return xx[0];
        }

        private string GetAscendant(double jd)
        {
            double[] cusps = new double[13];
            double[] ascmc = new double[10];
            _swe.swe_houses(jd, 0.0, 0.0, 'P', cusps, ascmc);
            return GetSign(ascmc[0]);
        }

        private string GetSign(double longitude)
        {
            int index = ((int)Math.Floor(longitude / 30.0)) % 12;
            if (index < 0) index += 12;
            return Signs[index];
        }
    }
}
