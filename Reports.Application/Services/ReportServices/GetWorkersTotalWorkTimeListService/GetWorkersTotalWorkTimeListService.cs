using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.FilterResults;
using Reports.Helpers.UtilityServices.Pagination;
using Reports.Helpers.UtilityServices.TimeFormat;
using static Reports.Helpers.UtilityServices.Pagination.Pagination;

namespace Reports.Application.Services.ReportServices.GetWorkersTotalWorkTimeListService
{
    public class GetWorkersTotalWorkTimeListService : IGetWorkersTotalWorkTimeListService
    {
        private readonly Context _context;

        public GetWorkersTotalWorkTimeListService(Context context)
        {
            _context = context;
        }

        public ResultDto<WorkersTotalWorkTimeResultDto> Execute(WorkersTotalWorkTimeRequestDto request)
        {
            DateTime? startPeriod = null;
            DateTime? finishPeriod = null;
            DateTime? searchKeyDate = null;

            bool periodIsSearched = false;
            bool specificDateIsSearched = false;

            var result = new ResultDto<WorkersTotalWorkTimeResultDto>(false, "")
            {
                Data = new WorkersTotalWorkTimeResultDto
                {
                    PeriodIsSearched = periodIsSearched,
                    SpecificDateIsSearched = specificDateIsSearched,
                    RequestedPageIndex = request.PageIndex,
                    ItemsInPageCount = request.ItemsInPageCount,
                    BaseWorkTime = request.BaseWorkTime,
                }
            };


            if (!String.IsNullOrEmpty(request.StartPeriod) && !String.IsNullOrEmpty(request.FinishPeriod))
            {
                periodIsSearched = true;
                startPeriod = request.StartPeriod.ShamsiStringToDateTime();
                finishPeriod = request.FinishPeriod.ShamsiStringToDateTime();

                if (startPeriod > finishPeriod)
                {
                    result.Message = "تاریخ پایان بازه نمی تواند قبل از شروع آن باشد!";
                    result.Data.StartPeriod = startPeriod?.ConvertMiladiToShamsi();
                    result.Data.FinishPeriod = finishPeriod?.ConvertMiladiToShamsi();
                    result.Data.RequestedSearchKeyDate = searchKeyDate?.ConvertMiladiToShamsi();

                    return result;
                }

            }
            else if (!String.IsNullOrEmpty(request.SearchKeyDate))
            {
                searchKeyDate = request.SearchKeyDate.ShamsiStringToDateTime();
                specificDateIsSearched = true;
            }
            else
            {
                periodIsSearched = true;
                startPeriod = DateTime.Now.Date.AddDays(-7);
                finishPeriod = DateTime.Now;
            }

            result.Data.StartPeriod = startPeriod?.ConvertMiladiToShamsi();
            result.Data.FinishPeriod = finishPeriod?.ConvertMiladiToShamsi();
            result.Data.RequestedSearchKeyDate = searchKeyDate?.ConvertMiladiToShamsi();

            var timeFiltersList = new List<IFilterService<Report>>();
            timeFiltersList.Add(new PeriodFilter(startPeriod, finishPeriod));
            timeFiltersList.Add(new SpecificDateFilter(searchKeyDate));



            var paginationResult = _context.Reports
                        .ApplyFilters(timeFiltersList)
                        .GroupBy(p => p.UserId)
                        .Select(p => new
                        {
                            UserId = p.Key,
                            IsRemote = p.First().IsRemote,
                            TotalWorkedMinutesInPeriod = p.Sum(p => p.TotalWorkedMinutes),
                            TotalRemoteWorkedMinutes = p.Where(u => u.IsRemote).Sum(u => u.TotalWorkedMinutes),
                        }).Where(p => p.TotalWorkedMinutesInPeriod > request.BaseWorkTime * 60)
                        .ToPaged(request.PageIndex, request.ItemsInPageCount);

            if (!paginationResult.Succeeded)
            {
                result.Message = paginationResult.Message;
                return result;
            }

            var usersList = _context.Users.Select(p => new
            {
                p.Id,
                p.FirstName,
                p.LastName,
            }).Where(u => paginationResult.RequestedPageList.Select(p => p.UserId).Contains(u.Id)).ToList();

            result.Data.WorkersList = new List<WorkerToShowInListDto>();

            foreach (var item in paginationResult.RequestedPageList)
            {
                result.Data.WorkersList.Add(new WorkerToShowInListDto()
                {
                    UserId = item.UserId,
                    RemoteWorkTime = TimeFormat.TotalMinutesToTimeFormat(item.TotalRemoteWorkedMinutes),
                    NoneRemoteWorkTime = TimeFormat.TotalMinutesToTimeFormat(item.TotalWorkedMinutesInPeriod - item.TotalRemoteWorkedMinutes),
                    WorkHour = item.TotalWorkedMinutesInPeriod / 60,
                    WorkTime = TimeFormat.TotalMinutesToTimeFormat(item.TotalWorkedMinutesInPeriod),
                    UsersFirstName = usersList.Single(p => p.Id == item.UserId).FirstName,
                    UsersLastName = usersList.Single(p => p.Id == item.UserId).LastName,
                });
            }

            result.Succeeded = true;
            result.Data.FirstPageIndexToShow = paginationResult.FirstPageIndexToShow;
            result.Data.LastPageIndexToShow = paginationResult.LastPageIndexToShow;
            result.Data.NextIsDisabled = paginationResult.NextIsDisabled;
            result.Data.PagesCount = paginationResult.PagesCount;
            result.Data.PrevIsDisabled = paginationResult.PrevIsDiabled;
            result.Data.PeriodIsSearched = periodIsSearched;
            result.Data.SpecificDateIsSearched = specificDateIsSearched;
            result.Data.RequestedPageIndex = request.PageIndex;
            result.Data.ItemsInPageCount = request.ItemsInPageCount;

            result.Message = specificDateIsSearched ? $"نمایش مجموع ساعت کار هر کارمند در تاریخ {searchKeyDate?.ConvertMiladiToShamsi()}" : $"نمایش ساعت کار مجموع کارکنان از تاریخ {startPeriod?.ConvertMiladiToShamsi()} تا تاریخ {finishPeriod?.ConvertMiladiToShamsi()}";

            return result;

        }

    }
}
