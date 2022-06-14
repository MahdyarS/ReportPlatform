using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Models.ReportsControllerModels
{
    public class EditPeriodViewModel
    {
        [Required]
        public int PeriodId { get; set; }
        [Required(ErrorMessage = "نام بازه الزامیست")]
        public string PeriodName { get; set; }
        [Required(ErrorMessage = "تاریخ شروع بازه الزامیست")]
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string StartPeriod { get; set; }
        [Required(ErrorMessage = "تاریخ پایان بازه الزامیست")]
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string FinishPeriod { get; set; }
        [Required(ErrorMessage = "توضیحات بازه الزامیست")]
        public string PeriodDescription { get; set; }
    }
}
