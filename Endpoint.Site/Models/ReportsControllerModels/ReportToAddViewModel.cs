namespace Endpoint.Site.Models.ReportsControllerModels
{
    public class ReportToAddViewModel
    {
        public string Date { get; set; }
        public TimeSpan? BeginningTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public string ReportsDetail { get; set; }
        public bool IsRemote { get; set; } = false;
    }
}
