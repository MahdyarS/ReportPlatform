using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD.PersianDateTime.Core;

namespace Reports.Helpers.UtilityServices.DateConversionService
{
    public static class DateConversionService
    {
        public static DateTime ShamsiStringToDateTime(this string shamsiDate)
        {
            string[] dateArray = shamsiDate.Split('/');

            PersianCalendar pc = new PersianCalendar();

            return new DateTime(Convert.ToInt32(dateArray[0]),
                                Convert.ToInt32(dateArray[1]),
                                Convert.ToInt32(dateArray[2]), pc);
        }

        public static string ConvertMiladiToShamsi(this DateTime date)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            var persian = persianDateTime.ToString("yyyy/MM/dd");
            char[] persianDigits = { '۰', '۱', '۲','۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            char[] englishDigits = { '0', '1', '2','3', '4', '5', '6', '7', '8', '9' };

            foreach (var character in persian)
            {
                for(int i = 0; i < 10; i++)
                {
                    if(character == persianDigits[i])
                    {
                        persian = persian.Replace(character, englishDigits[i]);
                        break;
                    }
                }
            }

            return persian;
        }
    }
}
