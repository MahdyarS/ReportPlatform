using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;

namespace Reports.Application.Services.PeriodServices.GetPeriodByIdService
{
    public class GetPeriodByIdService : IGetPeriodByIdService
    {
        private readonly Context _context;
        public GetPeriodByIdService(Context context)
        {
            _context = context;
        }

        public ResultDto<PeriodToShowDto> Execute(int periodId, string userName)
        {
            var period = _context.Periods.Include(p => p.User).SingleOrDefault(p => p.PeriodId == periodId);
            if (period == null)
                return new ResultDto<PeriodToShowDto>(false, "بازه مورد نظر یافت نشد!");
            if (period.User.UserName != userName)
                return new ResultDto<PeriodToShowDto>(false, "درخواست غیرمجاز");
            return new ResultDto<PeriodToShowDto>(true, "عملیات با موفقیت انجام شد!")
            {
                Data = new PeriodToShowDto
                {
                    PeriodId = periodId,
                    StartPeriodDate = period.StartPeriod.ConvertMiladiToShamsi(),
                    FinishPeriodDate = period.FinishPeriod.ConvertMiladiToShamsi(),
                    PeriodDescription = period.PeriodDescription,
                    PeriodName = period.PeriodName
                }
            };
        }
    }
}
