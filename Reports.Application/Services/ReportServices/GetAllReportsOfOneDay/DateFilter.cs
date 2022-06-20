using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.UtilityServices.FilterResults;

namespace Reports.Application.Services.ReportServices.GetAllReportsOfOneDay
{
    public class DateFilter : IFilterService<Report>
    {
        public DateTime Date { get; set; }

        public DateFilter(DateTime date)
        {
            Date = date;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            return source.Where(p => p.Date.Date == this.Date.Date);
        }
    }


}
