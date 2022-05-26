using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.DeleteReportService
{
    public interface IDeleteReportService
    {
        ResultDto Execute(int ReportId, string UserName);
    }

    public class DeleteReportService : IDeleteReportService
    {
        private readonly Context _context;

        public DeleteReportService(Context context)
        {
            _context = context;
        }

        public ResultDto Execute(int ReportId, string UserName)
        {
            var report = _context.Reports.Include(p => p.User).SingleOrDefault(p => p.ReportId == ReportId);
            if (report == null)
                return new ResultDto(false,"گزارش برای حذف یافت نشد!");

            if (report.User.UserName != UserName)
                return new ResultDto(false, "عملیات غیرمجاز!");

            _context.Reports.Remove(report);
            _context.SaveChanges();

            return new ResultDto(true, "گزارش مورد نظر با موفقیت حذف شد!");

        }
    }
}
