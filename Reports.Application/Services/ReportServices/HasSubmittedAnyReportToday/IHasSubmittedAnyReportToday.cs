using Reports.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.HasSubmittedAnyReportToday
{
    public interface IHasSubmittedAnyReportToday
    {
        bool Execute(string UserId);
    }
    public class HasSubmittedAnyReportToday : IHasSubmittedAnyReportToday
    {
        private readonly Context _context;

        public HasSubmittedAnyReportToday(Context context)
        {
            _context = context;
        }

        public bool Execute(string UserId)
        {
            return _context.Reports.Where(p => p.UserId == UserId && p.Date == DateTime.Now.Date).Any();
        }
    }
}
