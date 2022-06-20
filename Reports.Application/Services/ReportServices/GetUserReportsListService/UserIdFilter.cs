using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.UtilityServices.FilterResults;

namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public class UserIdFilter : IFilterService<Report>
    {
        public string UserId { get; set; }
        public UserIdFilter(string userId)
        {
            UserId = userId;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            if (!String.IsNullOrEmpty(this.UserId))
                return source.Where(p => p.UserId == this.UserId);
            return source;
        }
    }


}
