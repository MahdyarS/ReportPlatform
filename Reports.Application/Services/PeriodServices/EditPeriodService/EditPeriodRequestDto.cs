namespace Reports.Application.Services.PeriodServices.EditPeriodService
{
    public class EditPeriodRequestDto
    {
        public int PeriodId { get; set; }
        public string UserName { get; set; }
        public string PeriodName { get; set; }
        public string StartPeriod { get; set; }
        public string FinishPeriod { get; set; }
        public string PeriodDescription { get; set; }
    }
}
