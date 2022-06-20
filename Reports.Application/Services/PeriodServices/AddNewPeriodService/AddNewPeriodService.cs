using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;

namespace Reports.Application.Services.PeriodServices.AddNewPeriodService
{
    public class AddNewPeriodService : IAddNewPeriodService
    {
        private readonly Context _context;

        public AddNewPeriodService(Context context)
        {
            _context = context;
        }

        public ResultDto Execute(NewPeriodToAddRequestDto request)
        {
            if (String.IsNullOrEmpty(request.PeriodName) || String.IsNullOrEmpty(request.PeriodName) ||
                String.IsNullOrEmpty(request.PeriodName) || String.IsNullOrEmpty(request.PeriodName))
                return new ResultDto(false, "اطلاعات وارد شده کامل نیست!");
            var startDate = request.StartPeriodDate.ShamsiStringToDateTime();
            var finishtDate = request.FinishPeriodDate.ShamsiStringToDateTime();

            if (startDate > finishtDate)
                return new ResultDto(false, "تاریخ پایان بازه نمی تواند زود تر از شروع آن باشد!");

            var period = new Period
            {
                FinishPeriod = finishtDate,
                StartPeriod = startDate,
                PeriodDescription = request.PeriodDescription,
                PeriodName = request.PeriodName,
                User = _context.Users.SingleOrDefault(p => p.UserName == request.UserName)
            };

            _context.Periods.Add(period);
            _context.SaveChanges();

            return new ResultDto(true, "بازه مورد نظر با موفقیت ایجاد شد!");

        }
            
        
    }
}
