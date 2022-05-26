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

        public static string ConvertMiladiToShamsi(this DateTime date, string format)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.ToString(format);
        }
    }
}
