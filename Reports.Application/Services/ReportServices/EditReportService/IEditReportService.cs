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
            if (!request.IsRemote && (request.BeginningTime == null || request.FinishTime == null))
                return new ResultDto(false, "ساعت شروع و پایان کار الزامیست!");

            if (request.IsRemote && request.RemoteWorkedTime == null)
                return new ResultDto(false, "ساعت کار برای گزارش کار غیرحضوری الزامیست!");

            var report = _context.Reports.SingleOrDefault(p => p.ReportId == request.ReportId);

            if (report == null)
                return new ResultDto(false, "گزارشی جهت ویرایش پیدا نشد!");

            if (report.UserId != request.UserId)
                return new ResultDto(false,"شما مجاز به انجام عملیات درخواست شده نیستید!");

            DateTime date = request.Date.ShamsiStringToDateTime();
            if (date > DateTime.Now)
                return new ResultDto(false, "شما مجاز به ثبت گزارش برای روز های آینده نیستید!");

            if (date < DateTime.Now.AddDays(-7))
                return new ResultDto(false, "شما مجاز به ثبت یا تغییر گزارش های قبل تر از یک هفته نیستید!");

            if (request.BeginningTime > request.FinishTime)
                return new ResultDto(false, "زمان پایان کار نمی تواند قبل از شروع کار باشد!");

            if (!request.IsRemote)
            {
                report.StartWorkTime = request.BeginningTime.Value;
                report.TotalWorkedMinutes = (short)request.FinishTime.Value.Subtract(request.BeginningTime.Value).TotalMinutes;
            }
            else
                report.TotalWorkedMinutes = (short)request.RemoteWorkedTime.Value.TotalMinutes;

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
        public TimeSpan? RemoteWorkedTime { get; set; }
        public string ReportsDetail { get; set; }
        public bool IsRemote { get; set; }
    }
}
