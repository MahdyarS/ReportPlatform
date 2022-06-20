namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public class DateValidationResultDto
    {
        public bool SpecificDateIsSearched { get; set; } = false;
        public bool PeriodIsSearched { get; set; } = false;
        public bool IsInvalidPeriod { get; set; } = false;
        public DateTime? StartPeriod { get; set; }
        public DateTime? FinishPeriod { get; set; }
        public DateTime? SearchKeyDate { get; set; }
    }


}
