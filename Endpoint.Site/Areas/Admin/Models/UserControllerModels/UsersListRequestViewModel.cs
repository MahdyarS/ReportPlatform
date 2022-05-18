namespace Endpoint.Site.Areas.Admin.Models.UserControllerModels
{
    public class UsersListRequestViewModel
    {
        public string SearchKey { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 20;

    }
}
