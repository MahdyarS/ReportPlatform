using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Helpers.UtilityServices.TimeFormat
{
    public static class TimeFormat
    {
        public static string TotalMinutesToTimeFormat(int totalMinutes)
        {
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;
            string hoursString = hours < 10 ? "0" + hours.ToString() : hours.ToString();
            string minutesString = minutes < 10? "0" + minutes.ToString() : minutes.ToString();

            return hoursString + ":" + minutesString;
        }
    }
}
