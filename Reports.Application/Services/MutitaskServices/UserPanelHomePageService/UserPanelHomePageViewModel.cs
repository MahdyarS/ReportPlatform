using Reports.Application.Services.ReportServices.GetWorkTimeChart;

namespace Reports.Application.Services.MutitaskServices.UserPanelHomePageService
{
    public class UserPanelHomePageViewModel
    {
        public string UserId { get; set; }
        public bool SubmittedTodaysReport { get; set; }
        public ChartDataDto ChartData { get; set; }


    }
}
