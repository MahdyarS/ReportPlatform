using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Areas.Admin.Models.ReportsControllerModels
{
    public class AllWorkersWorkTimeChartRequestModel
    {
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string? StartDate { get; set; } = "";
        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))", ErrorMessage = "تاریخ وارد شده صحیح نیست!")]
        public string? FinishDate { get; set; } = "";
    }
}
