namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public class GetUsersReportsResultDto
    {
        public List<ReportToShowDto>? ReportsList { get; set; }
        public string TotalHoursWorkedInPeriod { get; set; }
        public int RequestedPageIndex { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public bool PeriodIsSearched { get; set; }
        public bool SpecificDateisSearched { get; set; }
        public string SearchKeyDate { get; set; } = "";
        public string StartPeriod { get; set; } = "";
        public string FinishPeriod { get; set; } = "";
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string PeriodName { get; set; }
        public bool HasRemoteReports { get; set; }
        public bool HasNoneRemoteReports { get; set; }
    }


}
