using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Reports;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.AddNewReportService
{
    public interface IAddNewReportService
    {
        Task<ResultDto> Execute(ReportToAddRequestDto request);
    }

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
            if (request.BeginningTime == null || request.FinishTime == null)
                return new ResultDto(false, "ساعت شروع و پایان کار الزامیست!");

            if (request.BeginningTime > request.FinishTime)
                return new ResultDto(false,"زمان پایان کار نمی تواند قبل از شروع کار باشد!");

            var date = request.Date.ShamsiStringToDateTime();

            if (date > DateTime.Now)
                return new ResultDto(false, "شما مجاز به ثبت گزارش روز های آینده نیستید!");

            if (date < DateTime.Now.AddDays(-7))
                return new ResultDto(false, "شما مجاز به ثبت گزارش در ناریخ های پیش از یک هفته قبل نیستید!");

            var user = await _userManager.FindByNameAsync(request.UserName);

            /*
            Report already = _context.Reports.Where(p => p.UserId == user.Id && p.Date == date).SingleOrDefault();

            if (already != null)
                return new ResultDto(false, "شما گزارش کار این تاریخ را نوشته اید!");
            */

            var report = new Report
            {
                Date = date,
                StartWorkTime = request.BeginningTime!.Value,
                FinishWorkTime = request.FinishTime!.Value,
                InsertionDateAndTime = DateTime.Now,
                User = user,
                ReportsDetail = request.ReportsDetail,
                IsRemote = request.IsRemote,
            };
            _context.Reports.Add(report);
            _context.SaveChanges();

            return new ResultDto(true,"گزارش با موفقیت ثبت شد!");
        }
    }

    public class ReportToAddRequestDto
    {
        public string Date { get; set; }
        public TimeSpan? BeginningTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public string ReportsDetail { get; set; }
        public string UserName { get; set; }
        public bool IsRemote { get; set; } = false;
    }
}
