using Reports.DataAccess.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.DataAccess.Entities.Reports
{
    public class Report
    {
        public int ReportId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public bool IsRemote { get; set; } = false;
        public DateTime Date { get; set; }
        public TimeSpan StartWorkTime { get; set; }
        public TimeSpan FinishWorkTime { get; set; }
        public string ReportsDetail { get; set; }
        public DateTime InsertionDateAndTime { get; set; }

    }
}
