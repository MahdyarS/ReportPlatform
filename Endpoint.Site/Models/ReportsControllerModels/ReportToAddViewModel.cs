using Endpoint.Site.Models.ValidationAttributes;
using Foolproof;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Models.ReportsControllerModels
{
    public class ReportToAddViewModel
    {
        [Required(ErrorMessage ="این فیلد الزامیست!")]
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))",ErrorMessage ="تاریخ وارد شده صحیح نیست!")]
        public string Date { get; set; }
        public TimeSpan? BeginningTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        [Range(minimum: 0, maximum: 23, ErrorMessage = "ساعت وارد شده صحیح نیست!")]
        //[RequiredIf("IsRemote", true,ErrorMessage ="دقیقه و ساعت کار برای گزارش غیرحضوری الزامیست!")]
        public int? RemoteWorkedHour { get; set; } = 0;
        [Range(minimum: 0, maximum: 59,ErrorMessage = "دقیقه وارد شده صحیح نیست!")]
        //[RequiredIf("IsRemote", true, ErrorMessage = "دقیقه و ساعت کار برای گزارش غیرحضوری الزامیست!")]
        public int? RemoteWorkedMinute { get; set; } = 0;
        [Required(ErrorMessage = "این فیلد الزامیست!")]
        public string ReportsDetail { get; set; }
        [Required(ErrorMessage = "این فیلد الزامیست!")]
        public bool IsRemote { get; set; } = false;
    }
}
