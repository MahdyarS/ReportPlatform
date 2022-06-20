namespace Reports.Application.Services.ReportServices.GetWorkersTotalWorkTimeListService
{
    public class WorkersTotalWorkTimeRequestDto
    {
        public string StartPeriod { get; set; }
        public string FinishPeriod { get; set; }
        public string SearchKeyDate { get; set; }
        public int PageIndex { get; set; }
        public int ItemsInPageCount { get; set; }
        public int BaseWorkTime { get; set; } = 0;
    }
}
