using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.UtilityServices.FilterResults;

namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public class RemoteOrNoneRemoteFilter : IFilterService<Report>
    {
        public bool HasRemoteReports { get; set; } = true;
        public bool HasNoneRemoteReports { get; set; } = true;

        public RemoteOrNoneRemoteFilter(bool hasRemoteReports, bool hasNoneRemoteReports)
        {
            HasRemoteReports = hasRemoteReports;
            HasNoneRemoteReports = hasNoneRemoteReports;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            if (!HasRemoteReports && !HasNoneRemoteReports)
                return source.Where(p => false);
            if (!HasRemoteReports && HasNoneRemoteReports)
                return source.Where(p => p.IsRemote == false);
            if (HasRemoteReports && !HasNoneRemoteReports)
                return source.Where(p => p.IsRemote);
            return source;
        }
    }


}
