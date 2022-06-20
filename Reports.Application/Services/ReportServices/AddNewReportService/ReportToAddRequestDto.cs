namespace Reports.Application.Services.ReportServices.AddNewReportService
{
    public class ReportToAddRequestDto
    {
        public string Date { get; set; }
        public TimeSpan? RemoteWorkedTime { get; set; }
        public TimeSpan? BeginningTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public string ReportsDetail { get; set; }
        public string UserId { get; set; }
        public bool IsRemote { get; set; } = false;
    }
}
