namespace Endpoint.Site.Models.ReportsControllerModels
{
    public class PeriodsListRequestModel
    {
        public string SearchKey { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 1;
    }
}
