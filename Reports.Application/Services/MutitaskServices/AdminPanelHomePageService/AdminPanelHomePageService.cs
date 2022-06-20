using Microsoft.EntityFrameworkCore;
using Reports.Application.Services.ReportServices.GetWorkTimeChart;
using Reports.DataAccess.Contexts;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.TimeFormat;

namespace Reports.Application.Services.MutitaskServices.AdminPanelHomePageService
{
    public class AdminPanelHomePageService : IAdminPanelHomePageService
    {
        private readonly Context _context;
        private readonly IGetWorkTimeChart _getWorkTimeChart;

        public AdminPanelHomePageService(Context context, IGetWorkTimeChart getWorkTimeChart)
        {
            _context = context;
            _getWorkTimeChart = getWorkTimeChart;
        }


        public AdminPanelHomePageViewModel Execute()
        {
            var chartResult = _getWorkTimeChart.Execute(new GetWorkTimeChartRequestDto
            {
                StartDate = DateTime.Now.AddDays(-10).ConvertMiladiToShamsi(),
                FinishDate = DateTime.Now.ConvertMiladiToShamsi(),
            });

            var result = new AdminPanelHomePageViewModel
            {
                ChartData = chartResult.Data.ChartData,
                RemoteChartData = chartResult.Data.RemoteChartData,
                NoneRemoteChartData = chartResult.Data.NoneRemoteChartData,
                LastReportsList = GetLastReports()
            };
            return result;
        }

        private List<ReportToShowInAdminHomeDto> GetLastReports()
        {
            int reportsCount = _context.Reports.Count();

            var query = _context.Reports.Include(p => p.User).AsQueryable();

            if (reportsCount > 10)
            {
                query = query.Skip(reportsCount - 10);
            }

            return query.Select(p => new ReportToShowInAdminHomeDto
            {
                Date = p.Date.ConvertMiladiToShamsi(),
                UsersFirstName = p.User.FirstName,
                UsersLastName = p.User.LastName,
                ReportsDetail = p.ReportsDetail,
                WorkTime = TimeFormat.TotalMinutesToTimeFormat(p.TotalWorkedMinutes),
                
            }).ToList();

        }
    }
}
