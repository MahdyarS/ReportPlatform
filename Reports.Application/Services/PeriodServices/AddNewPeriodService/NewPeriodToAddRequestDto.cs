namespace Reports.Application.Services.PeriodServices.AddNewPeriodService
{
    public class NewPeriodToAddRequestDto
    {
        public string UserName { get; set; }
        public string PeriodName { get; set; }
        public string StartPeriodDate { get; set; }
        public string FinishPeriodDate { get; set; }
        public string PeriodDescription { get; set; }
    }
}
