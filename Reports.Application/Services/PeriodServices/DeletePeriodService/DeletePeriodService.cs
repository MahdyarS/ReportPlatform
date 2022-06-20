using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;

namespace Reports.Application.Services.PeriodServices.DeletePeriodService
{
    public class DeletePeriodService : IDeletePeriodService
    {
        private readonly Context _context;

        public DeletePeriodService(Context context)
        {
            _context = context;
        }

        public ResultDto Execute(int PeriodId, string UserName)
        {
            var period = _context.Periods.Include(p => p.User).SingleOrDefault(p => p.PeriodId == PeriodId);
            if (period == null)
                return new ResultDto(false, "بازه مورد نظر پیدا نشد!");

            if (period.User.UserName != UserName)
                return new ResultDto(false, "عملیات غیرمجاز");

            period.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto(true, "بازه مورد نظر با موفقیت حذف شد!");
        }
    }
}
