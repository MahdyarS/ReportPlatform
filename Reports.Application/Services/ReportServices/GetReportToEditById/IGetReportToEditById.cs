using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.GetReportToEditById
{
    public interface IGetReportToEditById
    {
        ResultDto<ReportToShowForEditDto> Execute(int reportId,string userName);
    }

    public class GetReportToEditById : IGetReportToEditById
    {
        private readonly Context _context;

        public GetReportToEditById(Context context)
        {
            _context = context;
        }

        public ResultDto<ReportToShowForEditDto> Execute(int reportId, string userName)
        {
            var report = _context.Reports.Include(p => p.User).SingleOrDefault(p => p.ReportId == reportId);

            if (report == null)
                return new ResultDto<ReportToShowForEditDto>(false, "گزارش مورد نظر پیدا نشد!");

            if (report.User.UserName != userName)
                return new ResultDto<ReportToShowForEditDto>(false, "درخواست عملیات غیرمجاز!");

            return new ResultDto<ReportToShowForEditDto>(true, "")
            {
                Data = new ReportToShowForEditDto
                {
                    Date = report.Date.ConvertMiladiToShamsi("yyyy/MM/dd"),
                    BeginningTime = report.StartWorkTime,
                    FinishTime = report.FinishWorkTime,
                    ReportsDetail = report.ReportsDetail,
                    IsRemote = report.IsRemote
                }
            };

        }
    }

    public class ReportToShowForEditDto
    {
        public string Date { get; set; }
        public TimeSpan? BeginningTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public string ReportsDetail { get; set; }
        public bool IsRemote { get; set; } = false;
    }

}
