namespace Reports.Application.Services.ReportServices.EditReportService
{
    public class EditReportRequest
    {
        public int ReportId { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; }
        public TimeSpan? BeginningTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public TimeSpan? RemoteWorkedTime { get; set; }
        public string ReportsDetail { get; set; }
        public bool IsRemote { get; set; }
    }
}
