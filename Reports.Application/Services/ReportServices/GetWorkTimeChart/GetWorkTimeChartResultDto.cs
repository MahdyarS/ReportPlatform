namespace Reports.Application.Services.ReportServices.GetWorkTimeChart
{
    public class GetWorkTimeChartResultDto
    {
        public string UserId { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public ChartDataDto ChartData { get; set; }
        public ChartDataDto RemoteChartData { get; set; }
        public ChartDataDto NoneRemoteChartData { get; set; }
    }


}
