using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.UtilityServices.FilterResults;

namespace Reports.Application.Services.ReportServices.GetWorkersTotalWorkTimeListService
{
    public class SpecificDateFilter : IFilterService<Report>
    {
        public DateTime? SearchKeyDate { get; set; }

        public SpecificDateFilter(DateTime? searchKeyDate)
        {
            SearchKeyDate = searchKeyDate;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            if (this.SearchKeyDate == null)
                return source;
            return source.Where(p => p.Date == this.SearchKeyDate);
        }
    }
}
