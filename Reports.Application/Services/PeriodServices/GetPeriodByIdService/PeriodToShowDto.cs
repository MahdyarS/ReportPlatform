namespace Reports.Application.Services.PeriodServices.GetPeriodByIdService
{
    public class PeriodToShowDto
    {
        public int PeriodId { get; set; }
        public string PeriodName { get; set; }
        public string StartPeriodDate { get; set; }
        public string FinishPeriodDate { get; set; }
        public string PeriodDescription { get; set; }
    }
}
