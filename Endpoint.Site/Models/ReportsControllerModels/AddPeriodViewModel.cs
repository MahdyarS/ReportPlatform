using Reports.Helpers.UtilityServices.DateConversionService;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Models.ReportsControllerModels
{
    public class AddPeriodViewModel
    {
        [Required(ErrorMessage = "نام بازه الزامیست!")]
        public string PeriodName { get; set; }
        [Required(ErrorMessage = "تاریخ شروع بازه الزامیست!")]
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string StartPeriodDate { get; set; }
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        [Required(ErrorMessage = "تاریخ پایان بازه الزامیست!")]
        public string FinishPeriodDate { get; set; }
        [Required(ErrorMessage = "توضیحات بازه الزامیست!")]
        public string PeriodDescription { get; set; }
    }
}
