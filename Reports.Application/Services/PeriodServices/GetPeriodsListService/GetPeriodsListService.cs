using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.Pagination;

namespace Reports.Application.Services.PeriodServices.GetPeriodsListService
{
    public class GetPeriodsListService : IGetPeriodsListService
    {
        private readonly Context _context;

        public GetPeriodsListService(Context context)
        {
            _context = context;
        }

        public ResultDto<GetPeriodsListResultDto> Execute(GetPeriodListRequestDto request)
        {
            var query = _context.Periods.Where(p => p.PeriodName.Contains(request.SearchKey) && p.UserId == request.UserId).OrderByDescending(p => p.PeriodId);

            var paginationResult = query.ToPaged(request.PageIndex, request.ItemsInPageCount);
                                          
            if (!paginationResult.Succeeded)
                return new ResultDto<GetPeriodsListResultDto>(false, paginationResult.Message)
                {
                    Data = new GetPeriodsListResultDto
                    {
                        UsersFirstName = request.UsersFirstName,
                        UsersLastName = request.UsersLastName,
                        UserId = request.UserId
                    }
                };

            return new ResultDto<GetPeriodsListResultDto>(true, "")
            {
                Data = new GetPeriodsListResultDto
                {
                    PeriodsList = paginationResult.RequestedPageList.Select(p => new PeriodInListDto
                    {
                        PeriodId = p.PeriodId,
                        PeriodName = p.PeriodName,
                        StartPeriodDate = p.StartPeriod.ConvertMiladiToShamsi(),
                        FinishPeriodDate = p.FinishPeriod.ConvertMiladiToShamsi(),
                        PeriodDescription = p.PeriodDescription
                    }).OrderByDescending(p => p.StartPeriodDate).ToList(),
                    FirstPageIndexToShow = paginationResult.FirstPageIndexToShow,
                    LastPageIndexToShow = paginationResult.LastPageIndexToShow,
                    NextIsDisabled = paginationResult.NextIsDisabled,
                    PagesCount = paginationResult.PagesCount,
                    PrevIsDisabled = paginationResult.PrevIsDiabled,
                    RequestedPageIndex = paginationResult.RequestedPageIndex,
                    RequestedSearchKey = request.SearchKey,
                    UserId = request.UserId,
                    UsersFirstName = request.UsersFirstName,
                    UsersLastName = request.UsersLastName,
                    
                }
            };

        }
    }
}
