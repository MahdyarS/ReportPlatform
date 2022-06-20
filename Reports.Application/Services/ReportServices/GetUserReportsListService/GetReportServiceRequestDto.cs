namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public class GetReportServiceRequestDto
    {
        public string PeriodName { get; set; } = "";
        public string UsersFirstName { get; set; } = "";
        public string UsersLastName { get; set; } = "";
        public string SearchKeyDate { get; set; } = "";
        public string StartPreriod { get; set; } = "";
        public string FinishPreriod { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 1;
        public string UserId { get; set; }
        public bool HasRemoteReports { get; set; } = true;
        public bool HasNoneRemoteReports { get; set; } = true;
    }


}
