namespace Reports.Application.Services.PeriodServices.GetPeriodsListService
{
    public class PeriodInListDto
    {
        public int PeriodId { get; set; }
        public string PeriodName { get; set; }
        public string StartPeriodDate { get; set; }
        public string FinishPeriodDate { get; set; }
        public string PeriodDescription { get; set; }
    }
}
