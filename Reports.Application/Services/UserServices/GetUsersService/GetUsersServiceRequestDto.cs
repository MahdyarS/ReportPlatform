namespace Reports.Application.Services.UserServices.GetUsersService
{
    public class GetUsersServiceRequestDto
    {
        public string SearchKey { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 20;
    }

}
