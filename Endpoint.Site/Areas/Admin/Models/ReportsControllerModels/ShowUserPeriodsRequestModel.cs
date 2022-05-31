namespace Endpoint.Site.Areas.Admin.Models.ReportsControllerModels
{
    public class ShowUserPeriodsRequestModel
    {
        public string SearchKey { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 1;
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
    }
}
