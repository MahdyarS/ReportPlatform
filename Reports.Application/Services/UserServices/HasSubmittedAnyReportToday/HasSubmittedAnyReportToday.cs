using Reports.DataAccess.Contexts;

namespace Reports.Application.Services.ReportServices.HasSubmittedAnyReportToday
{
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
