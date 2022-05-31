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
        public string StartPeriod { get; set; }
        [Required(ErrorMessage = "تاریخ پایان بازه الزامیست")]
        public string FinishPeriod { get; set; }
        [Required(ErrorMessage = "توضیحات بازه الزامیست")]
        public string PeriodDescription { get; set; }
    }
}
