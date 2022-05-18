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
        public DateTime Date { get; set; }
        public byte StartWorkHour { get; set; }
        public byte StartWorkMinute { get; set; }
        public byte FinishWorkHour { get; set; }
        public byte FinishWorkMinute { get; set; }
        public string ReportsDetail { get; set; }
        
    }
}
