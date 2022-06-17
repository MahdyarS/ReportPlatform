using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Areas.Admin.Models.ReportsControllerModels
{
    public class GetReportsOfEachDayRequestModel
    {
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string? Date { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 20;
        public int BaseWorkTime { get; set; } = 0;
        public string? HasRemoteReports { get; set; } = "";
        public string? HasNoneRemoteReports { get; set; } = "";

    }
}


