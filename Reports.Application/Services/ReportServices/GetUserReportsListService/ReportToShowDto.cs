namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public class ReportToShowDto
    {
        public int ReportId { get; set; }
        public string Date { get; set; }
        public string StartWorkTime { get; set; }
        public string FinishWorkTime { get; set; }
        public string WorkTime { get; set; }
        public string ReportsDetail { get; set; }
        public string IsRemote { get; set; }
    }


}
