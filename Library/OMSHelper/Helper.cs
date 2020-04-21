using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.OMSHelper
{
    public static class Helper
    {
        public static string GetCurrentSeason()
        {
            if (DateTime.Now.Month >= 9)
            {
                return DateTime.Now.Year.ToString() + "/" + (DateTime.Now.Year + 1).ToString();
            }
            else
            {
                return (DateTime.Now.Year - 1).ToString() + "/" + DateTime.Now.Year;
            }
        }

        public static string GetPrevSeason(string season)
        { 
            int year = 0;
            if (int.TryParse(season.Substring(0, 4), out year) && year > 2010)
            {
                return (year - 1).ToString() + "/" + year.ToString();
            }
            else 
            {
                return string.Empty;
            }
        }

        public static string GetPrevSeasonExt(string season, int number)
        {
            int year = 0;
            if (int.TryParse(season.Substring(0, 4), out year) && year > 2010)
            {
                return (year - number).ToString() + "/" + year.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetNextSeason(string season)
        {
            int year = 0;
            if (int.TryParse(season.Substring(0, 4), out year) && year > 2010)
            {
                return (year + 1).ToString() + "/" + (year + 2).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string AddPrefix(this string input, string repeatedChar, int stringLength)
        {
            string tmp = input;
            while (tmp.Length < stringLength)
            {
                tmp = repeatedChar + tmp;
            }

            return tmp;
        }
    }
}
