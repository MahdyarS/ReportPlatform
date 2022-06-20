namespace Reports.Application.Services.ReportServices.GetWorkersTotalWorkTimeListService
{
    public class WorkersTotalWorkTimeResultDto
    {
        public List<WorkerToShowInListDto> WorkersList { get; set; }
        public int RequestedPageIndex { get; set; }
        public string RequestedSearchKeyDate { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public int ItemsInPageCount { get; set; }
        public string StartPeriod { get; set; }
        public string FinishPeriod { get; set; }
        public bool PeriodIsSearched { get; set; } = false;
        public bool SpecificDateIsSearched { get; set; } = false;
        public int BaseWorkTime { get; set; }
    }
}
