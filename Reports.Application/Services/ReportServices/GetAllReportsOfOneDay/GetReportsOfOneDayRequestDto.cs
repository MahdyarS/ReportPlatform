namespace Reports.Application.Services.ReportServices.GetAllReportsOfOneDay
{
    public class GetReportsOfOneDayRequestDto
    {
        public string Date { get; set; } = "";
        public bool HasRemoteReports { get; set; } = true;
        public bool HasNoneRemoteReports { get; set; } = true;
        public int RequestedPageIndex { get; set; }
        public int ItemsInPageCount { get; set; } = 1;
    }


}
