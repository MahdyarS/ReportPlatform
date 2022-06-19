using Reports.DataAccess.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.DataAccess.Entities.Reports
{
    public class Period
    {
        public int PeriodId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string PeriodName { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime FinishPeriod { get; set; }
        public string PeriodDescription { get; set; }
        public bool IsRemoved { get; set; } = false;
    }
}
