using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Areas.Admin.Models.ReportsControllerModels
{
    public class GetOneUsersReportsViewModelRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UsersFirstName { get; set; }
        [Required]
        public string UsersLastName { get; set; }
        public string? PeriodName { get; set; } = "";
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string? SearchKeyDate { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string? StartPeriod { get; set; } = "";
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string? FinishPeriod { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 30;
        public string? HasRemoteReports { get; set; } = "";
        public string? HasNoneRemoteReports { get; set; } = "";
    }
}
