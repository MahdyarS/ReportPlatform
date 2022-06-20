namespace Reports.Application.Services.ReportServices.GetAllReportsOfOneDay
{
    public class GetAllReportsOfOneDayResultDto
    {
        public List<ReportToShowDto> ReportsList { get; set; }
        public int RequestedPageIndex { get; set; }
        public int RequestedItemsInPageCount { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public string SearchKeyDate { get; set; }
        public bool HasRemoteReports { get; set; }
        public bool HasNoneRemoteReports { get; set; }
    }


}
