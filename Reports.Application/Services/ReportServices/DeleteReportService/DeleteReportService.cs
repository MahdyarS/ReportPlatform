using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;

namespace Reports.Application.Services.ReportServices.DeleteReportService
{
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

            report.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto(true, "گزارش مورد نظر با موفقیت حذف شد!");

        }
    }
}
