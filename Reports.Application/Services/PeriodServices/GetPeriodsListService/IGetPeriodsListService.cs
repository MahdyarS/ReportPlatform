using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.PeriodServices.GetPeriodsListService
{
    public interface IGetPeriodsListService
    {
        ResultDto<GetPeriodsListResultDto> Execute(GetPeriodListRequestDto request);
    }

    public class GetPeriodsListService : IGetPeriodsListService
    {
        private readonly Context _context;

        public GetPeriodsListService(Context context)
        {
            _context = context;
        }

        public ResultDto<GetPeriodsListResultDto> Execute(GetPeriodListRequestDto request)
        {
            var query = _context.Periods.Where(p => p.PeriodName.Contains(request.SearchKey) && p.UserId == request.UserId);

            var paginationResult = query.Select(p => new PeriodInListDto
                                          {
                                              PeriodId = p.PeriodId,
                                              PeriodName = p.PeriodName,
                                              StartPeriodDate = p.StartPeriod.ConvertMiladiToShamsi(),
                                              FinishPeriodDate = p.FinishPeriod.ConvertMiladiToShamsi(),
                                              PeriodDescription = p.PeriodDescription
                                          }).OrderByDescending(p => p.StartPeriodDate)
                                          .ToPaged(request.PageIndex, request.ItemsInPageCount);

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
                    PeriodsList = paginationResult.RequestedPageList,
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

    public class GetPeriodsListResultDto
    {
        public List<PeriodInListDto>? PeriodsList { get; set; }
        public int RequestedPageIndex { get; set; }
        public string RequestedSearchKey { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
    }

    public class PeriodInListDto
    {
        public int PeriodId { get; set; }
        public string PeriodName { get; set; }
        public string StartPeriodDate { get; set; }
        public string FinishPeriodDate { get; set; }
        public string PeriodDescription { get; set; }
    }

    public class GetPeriodListRequestDto
    {
        public string SearchKey { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 1;
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }

    }
}
