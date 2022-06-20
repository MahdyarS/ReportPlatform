using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.UtilityServices.FilterResults;

namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public class PeriodFilter : IFilterService<Report>
    {
        public DateTime? StartPeriod { get; set; }
        public DateTime? FinishPeriod { get; set; }

        public PeriodFilter(DateTime? startPeriod, DateTime? finishPeriod)
        {
            StartPeriod = startPeriod;
            FinishPeriod = finishPeriod;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            if (this.StartPeriod == null || this.FinishPeriod == null)
                return source;
            return source.Where(p => p.Date >= this.StartPeriod && p.Date <= this.FinishPeriod);

        }
    }


}
