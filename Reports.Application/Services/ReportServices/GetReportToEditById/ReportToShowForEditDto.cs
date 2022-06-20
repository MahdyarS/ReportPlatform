namespace Reports.Application.Services.ReportServices.GetReportToEditById
{
    public class ReportToShowForEditDto
    {
        public string Date { get; set; }
        public TimeSpan? BeginningTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public int RemoteWorkedHour { get; set; } = 0;
        public int RemoteWorkedMinute { get; set; } = 0;
        public string ReportsDetail { get; set; }
        public bool IsRemote { get; set; } = false;
    }

}
