namespace Reports.Application.Services.UserServices.GetUsersService
{
    public class GetUsersResultDto
    {
        public List<UserModelInAdminList>? UsersList { get; set; }
        public int RequestedPageIndex { get; set; }
        public string RequestedSearchKey { get; set; }
        public bool PrevIsDiabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }

    }

}
