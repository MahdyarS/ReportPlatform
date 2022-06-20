using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Reports;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;

namespace Reports.Application.Services.ReportServices.AddNewReportService
{
    public class AddNewReportService : IAddNewReportService
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;

        public AddNewReportService(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ResultDto> Execute(ReportToAddRequestDto request)
        {
            if (!request.IsRemote && (request.BeginningTime == null || request.FinishTime == null))
                return new ResultDto(false, "ساعت شروع و پایان برای گزارش کار حضوری الزامیست!");

            if (request.IsRemote && request.RemoteWorkedTime == null)
                return new ResultDto(false, "ساعت کار برای گزارش کار غیرحضوری الزامیست!");

            if (request.BeginningTime > request.FinishTime)
                return new ResultDto(false,"زمان پایان کار نمی تواند قبل از شروع کار باشد!");

            var date = request.Date.ShamsiStringToDateTime();

            if (date > DateTime.Now)
                return new ResultDto(false, "شما مجاز به ثبت گزارش روز های آینده نیستید!");

            if (date < DateTime.Now.AddDays(-7))
                return new ResultDto(false, "شما مجاز به ثبت گزارش در ناریخ های پیش از یک هفته قبل نیستید!");



            var report = new Report
            {
                Date = date,
                StartWorkTime = request.BeginningTime,
                InsertionDateAndTime = DateTime.Now,
                UserId = request.UserId,
                ReportsDetail = request.ReportsDetail,
                IsRemote = request.IsRemote,
            };

            if (request.IsRemote)
                report.TotalWorkedMinutes = (short)request.RemoteWorkedTime!.Value.TotalMinutes;
            else
                report.TotalWorkedMinutes = (short)request.FinishTime.Value.Subtract(request.BeginningTime!.Value).TotalMinutes;
            

            _context.Reports.Add(report);
            _context.SaveChanges();

            return new ResultDto(true,"گزارش با موفقیت ثبت شد!");
        }
    }
}
