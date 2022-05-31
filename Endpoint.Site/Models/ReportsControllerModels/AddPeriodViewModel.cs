using Reports.Helpers.UtilityServices.DateConversionService;

namespace Endpoint.Site.Models.ReportsControllerModels
{
    public class AddPeriodViewModel
    {
        public string PeriodName { get; set; }
        public string StartPeriodDate { get; set; }
        public string FinishPeriodDate { get; set; }
        public string PeriodDescription { get; set; }
    }
}
