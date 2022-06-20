using Reports.Application.Services.ReportServices.GetWorkTimeChart;
using Reports.Application.Services.ReportServices.HasSubmittedAnyReportToday;
using Reports.Helpers.UtilityServices.DateConversionService;

namespace Reports.Application.Services.MutitaskServices.UserPanelHomePageService
{
    public class UserPanelHomePageService : IUserPanelHomePageService
    {
        private readonly IHasSubmittedAnyReportToday _hasSubmittedAnyReportToday;
        private readonly IGetWorkTimeChart _getWorkTimeChart;

        public UserPanelHomePageService(IHasSubmittedAnyReportToday hasSubmittedAnyReportToday, IGetWorkTimeChart getWorkTimeChart)
        {
            _hasSubmittedAnyReportToday = hasSubmittedAnyReportToday;
            _getWorkTimeChart = getWorkTimeChart;
        }

        public UserPanelHomePageViewModel Execute(string userId)
        {
            var result = new UserPanelHomePageViewModel
            {
                UserId = userId,
                SubmittedTodaysReport = _hasSubmittedAnyReportToday.Execute(userId),
                ChartData = _getWorkTimeChart.Execute(new GetWorkTimeChartRequestDto
                {
                    UserId = userId,
                    StartDate = DateTime.Now.AddDays(-10).ConvertMiladiToShamsi(),
                    FinishDate = DateTime.Now.ConvertMiladiToShamsi()
                }).Data.ChartData
            };

            return result;
        }
    }
}
