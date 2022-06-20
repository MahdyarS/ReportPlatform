namespace Reports.Application.Services.PeriodServices.GetPeriodsListService
{
    public class GetPeriodsListResultDto
    {
        public List<PeriodInListDto>? PeriodsList { get; set; }
        public int RequestedPageIndex { get; set; }
        public string RequestedSearchKey { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
    }
}
