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

        public ResultDto<ReportToShowForEditDto> Execute(int reportId, string userId)
        {
            var report = _context.Reports.SingleOrDefault(p => p.ReportId == reportId);

            if (report == null)
                return new ResultDto<ReportToShowForEditDto>(false, "گزارش مورد نظر پیدا نشد!");

            if (report.UserId != userId)
                return new ResultDto<ReportToShowForEditDto>(false, "درخواست عملیات غیرمجاز!");

            if (report.Date.Date < DateTime.Now.Date.AddDays(-7))
                return new ResultDto<ReportToShowForEditDto>(false, "شما مجاز به ویرایش گزارش های قبل تر از یک هفته پیش نیستید!");

            return new ResultDto<ReportToShowForEditDto>(true, "")
            {
                Data = new ReportToShowForEditDto
                {
                    Date = report.Date.ConvertMiladiToShamsi(),
                    BeginningTime = report.StartWorkTime,
                    FinishTime = !report.IsRemote ? report.StartWorkTime!.Value.Add(new TimeSpan(0,report.TotalWorkedMinutes,0)) : null,
                    RemoteWorkedHour = report.IsRemote ? report.TotalWorkedMinutes / 60 : 0,
                    RemoteWorkedMinute = report.IsRemote ? report.TotalWorkedMinutes % 60 : 0,
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
        public int RemoteWorkedHour { get; set; } = 0;
        public int RemoteWorkedMinute { get; set; } = 0;
        public string ReportsDetail { get; set; }
        public bool IsRemote { get; set; } = false;
    }

}
