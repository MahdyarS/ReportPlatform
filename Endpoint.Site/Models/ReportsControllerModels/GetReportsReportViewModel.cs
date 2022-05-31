namespace Endpoint.Site.Models.ReportsControllerModels
{
    public class GetReportsReportViewModelViewModel
    {
        public string PeriodName { get; set; } = "";
        public string SearchKeyDate { get; set; } = "";
        public string SearchKeyStartPreriodDate { get; set; } = "";
        public string SearchKeyFinishPreriodDate { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 1;
    }
}
