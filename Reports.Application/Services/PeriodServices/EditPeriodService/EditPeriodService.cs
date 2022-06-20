using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;

namespace Reports.Application.Services.PeriodServices.EditPeriodService
{
    public class EditPeriodService : IEditPeriodService
    {
        private readonly Context _context;

        public EditPeriodService(Context context)
        {
            _context = context;
        }

        public ResultDto Execute(EditPeriodRequestDto request)
        {
            var period = _context.Periods.Include(p => p.User).SingleOrDefault(p => p.PeriodId == request.PeriodId);
            if (period == null)
                return new ResultDto(false, "گزارش مورد نظر یافت نشد!");
            if (period.User.UserName != request.UserName)
                return new ResultDto(false,"عملیات غیرمجاز!");
            var startDate = request.StartPeriod.ShamsiStringToDateTime();
            var finishDate = request.FinishPeriod.ShamsiStringToDateTime();

            if (finishDate < startDate)
                return new ResultDto(false, "تاریخ پایان بازه نمی تواند زود تر از پایان آن باشد!");

            period.StartPeriod = startDate;
            period.FinishPeriod = finishDate;
            period.PeriodName = request.PeriodName;
            period.PeriodDescription = request.PeriodDescription;

            _context.SaveChanges();

            return new ResultDto(true, "بازه مورد نظر با موفقیت ویرایش شد!");
            
        }
    }
}
