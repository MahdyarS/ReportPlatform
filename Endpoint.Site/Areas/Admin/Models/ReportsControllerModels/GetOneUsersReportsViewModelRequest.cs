namespace Endpoint.Site.Areas.Admin.Models.ReportsControllerModels
{
    public class GetOneUsersReportsViewModelRequest
    {
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string PeriodName { get; set; } = "";
        public string SearchKeyDate { get; set; } = "";
        public string SearchKeyStartPreriodDate { get; set; } = "";
        public string SearchKeyFinishPreriodDate { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 1;
    }
}
