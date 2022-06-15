using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.EditReportService
{
    public interface IEditReportService
    {
        ResultDto Execute(EditReportRequest request);
    }

    public class EditReportService : IEditReportService
    {
        private readonly Context _context;

        public EditReportService(Context context)
        {
            _context = context;
        }

        public ResultDto Execute(EditReportRequest request)
        {
            if (request.BeginningTime == null || request.FinishTime == null)
                return new ResultDto(false, "ساعت شروع و پایان کار الزامیست!");

            var report = _context.Reports.SingleOrDefault(p => p.ReportId == request.ReportId);

            if (report == null)
                return new ResultDto(false, "گزارشی جهت ویرایش پیدا نشد!");

            if (report.UserId != request.UserId)
                return new ResultDto(false,"شما مجاز به انجام عملیات درخواست شده نیستید!");

            DateTime date = request.Date.ShamsiStringToDateTime();
            if (date > DateTime.Now)
                return new ResultDto(false, "شما مجاز به ثبت گزارش برای روز های آینده نیستید!");

            if (request.BeginningTime > request.FinishTime)
                return new ResultDto(false, "زمان پایان کار نمی تواند قبل از شروع کار باشد!");

            report.StartWorkTime = request.BeginningTime.Value;
            report.FinishWorkTime = request.FinishTime.Value;
            report.ReportsDetail = request.ReportsDetail;
            report.IsRemote = request.IsRemote;
            report.Date = date;

            _context.SaveChanges();

            return new ResultDto(true, "گزارش شما با موفقیت ویرایش شد!");

        }
    }

    public class EditReportRequest
    {
        public int ReportId { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; }
        public TimeSpan? BeginningTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public string ReportsDetail { get; set; }
        public bool IsRemote { get; set; }
    }
}
