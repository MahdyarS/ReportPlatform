using Reports.Application.Services.ReportServices.GetWorkTimeChart;

namespace Reports.Application.Services.MutitaskServices.AdminPanelHomePageService
{
    public class AdminPanelHomePageViewModel
    {
        public List<ReportToShowInAdminHomeDto> LastReportsList { get; set; }
        public ChartDataDto ChartData { get; set; }
        public ChartDataDto RemoteChartData { get; set; }
        public ChartDataDto NoneRemoteChartData { get; set; }
    }
}
