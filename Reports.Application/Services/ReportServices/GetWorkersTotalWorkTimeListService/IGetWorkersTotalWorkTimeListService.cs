using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.FilterResults;
using Reports.Helpers.UtilityServices.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reports.Helpers.UtilityServices.Pagination.Pagination;

namespace Reports.Application.Services.ReportServices.GetWorkersTotalWorkTimeListService
{
    public interface IGetWorkersTotalWorkTimeListService
    {
        ResultDto<WorkersTotalWorkTimeResultDto> Execute(WorkersTotalWorkTimeRequestDto request);
    }

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

            if (!String.IsNullOrEmpty(request.StartPeriod) && !String.IsNullOrEmpty(request.FinishPeriod))
            {
                periodIsSearched = true;
                startPeriod = request.StartPeriod.ShamsiStringToDateTime();
                finishPeriod = request.FinishPeriod.ShamsiStringToDateTime();

                if (startPeriod > finishPeriod)
                    return new ResultDto<WorkersTotalWorkTimeResultDto>(false, "تاریخ پایان بازه نمی تواند قبل از شروع آن باشد!")
                    {
                        Data = new WorkersTotalWorkTimeResultDto
                        {
                            StartPeriod = startPeriod?.ConvertMiladiToShamsi(),
                            FinishPeriod = finishPeriod?.ConvertMiladiToShamsi(),
                            RequestedSearchKeyDate = searchKeyDate?.ConvertMiladiToShamsi(),
                            PeriodIsSearched = periodIsSearched,
                            SpecificDateIsSearched = specificDateIsSearched,
                            RequestedPageIndex = request.PageIndex,
                            ItemsInPageCount = request.ItemsInPageCount,
                        }

                    };

            }
            else if (!String.IsNullOrEmpty(request.SearchKeyDate))
            {
                searchKeyDate = request.SearchKeyDate.ShamsiStringToDateTime();
                specificDateIsSearched = true;
            }
            else
            {
                periodIsSearched = true;
                startPeriod = DateTime.Now.AddDays(-7);
                finishPeriod = DateTime.Now;
            }

            var timeFiltersList = new List<IFilterService<Report>>();
            timeFiltersList.Add(new PeriodFilter(startPeriod, finishPeriod));
            timeFiltersList.Add(new SpecificDateFilter(searchKeyDate));


            var neededDataFromReportsInPeriod = _context.Reports.Include(p => p.User)
                                                    .ApplyFilters(timeFiltersList)
                                                    .Select(p => new
                                                    {
                                                        p.UserId,
                                                        UsersFirstName = p.User.FirstName,
                                                        UsersLastName = p.User.LastName,
                                                        ReportsWorkTime = p.FinishWorkTime.Subtract(p.StartWorkTime),
                                                        p.IsRemote
                                                    });

            var groupedResult = (from p in neededDataFromReportsInPeriod
                                 group p by p.UserId into r
                                 select r).ToList();

            var workersList = new List<WorkerToShowInListDto>();

            foreach (var item in groupedResult)
            {
                double totalMinutes = 0;
                double totalRemoteTimeMinutes = 0;
                double totalNoneRemoteTimeMinutes = 0;
                foreach (var workTimeSample in item)
                {
                    totalMinutes += workTimeSample.ReportsWorkTime.TotalMinutes;

                    if(workTimeSample.IsRemote)
                        totalRemoteTimeMinutes += workTimeSample.ReportsWorkTime.TotalMinutes;
                    else
                        totalNoneRemoteTimeMinutes += workTimeSample.ReportsWorkTime.TotalMinutes;
                }
                int totalHours = (int)totalMinutes / 60;
                int totalRemoteHours = (int)totalRemoteTimeMinutes / 60;
                int totalNoneRemoteHours = (int)totalNoneRemoteTimeMinutes / 60;

                int Minutes = (int)totalMinutes % 60;
                string minutesString = Minutes < 10 ? "0" + Minutes.ToString() : Minutes.ToString();
                string hoursString = totalHours < 10 ? "0" + totalHours.ToString() : totalHours.ToString();

                int remoteMinutes = (int)totalRemoteTimeMinutes % 60;
                string remoteMinutesString = remoteMinutes < 10 ? "0" + remoteMinutes.ToString() : remoteMinutes.ToString();
                string remoteHoursString = totalRemoteHours < 10 ? "0" + totalRemoteHours.ToString() : totalRemoteHours.ToString();

                int noneRemoteMinutes = (int)totalNoneRemoteTimeMinutes % 60;
                string noneRemoteMinutesString = noneRemoteMinutes < 10 ? "0" + noneRemoteMinutes.ToString() : noneRemoteMinutes.ToString();
                string noneRemoteHoursString = totalNoneRemoteHours < 10 ? "0" + totalNoneRemoteHours.ToString() : totalNoneRemoteHours.ToString();

                workersList.Add(new WorkerToShowInListDto()
                {
                    UserId = item.Key,
                    UsersFirstName = item.FirstOrDefault()?.UsersFirstName,
                    UsersLastName = item.FirstOrDefault()?.UsersLastName,
                    WorkHour = totalHours,
                    WorkTime = hoursString + ":" + minutesString,
                    RemoteWorkTime = remoteHoursString + ":" + remoteMinutesString,
                    NoneRemoteWorkTime = noneRemoteHoursString + ":" + noneRemoteMinutesString
                });
            }

            var workTimeFiltersList = new List<IFilterService<WorkerToShowInListDto>>();
            workTimeFiltersList.Add(new BaseWorkTimeFilter(request.BaseWorkTime));


            var paginationResult = workersList
                                    .ApplyFilters(workTimeFiltersList)
                                    .OrderByDescending(x => x.UsersLastName)
                                    .ThenBy(x => x.UsersFirstName)
                                    .ToPaged(request.PageIndex, request.ItemsInPageCount);

            if (!paginationResult.Succeeded)
                return new ResultDto<WorkersTotalWorkTimeResultDto>(false, paginationResult.Message)
                {
                    Data = new WorkersTotalWorkTimeResultDto
                    {
                        StartPeriod = startPeriod?.ConvertMiladiToShamsi(),
                        FinishPeriod = finishPeriod?.ConvertMiladiToShamsi(),
                        RequestedSearchKeyDate = searchKeyDate?.ConvertMiladiToShamsi(),
                        PeriodIsSearched = periodIsSearched,
                        SpecificDateIsSearched = specificDateIsSearched,
                        RequestedPageIndex = request.PageIndex,
                        ItemsInPageCount = request.ItemsInPageCount

                    }
                };


            return new ResultDto<WorkersTotalWorkTimeResultDto>(true, specificDateIsSearched ? $"نمایش مجموع ساعت کار هر کارمند در تاریخ {searchKeyDate?.ConvertMiladiToShamsi()}" : $"نمایش ساعت کار مجموع کارکنان از تاریخ {startPeriod?.ConvertMiladiToShamsi()} تا تاریخ {finishPeriod?.ConvertMiladiToShamsi()}")
            {
                Data = new WorkersTotalWorkTimeResultDto
                {
                    WorkersList = paginationResult.RequestedPageList,
                    StartPeriod = startPeriod?.ConvertMiladiToShamsi(),
                    FinishPeriod = finishPeriod?.ConvertMiladiToShamsi(),
                    RequestedSearchKeyDate = searchKeyDate?.ConvertMiladiToShamsi(),
                    FirstPageIndexToShow = paginationResult.FirstPageIndexToShow,
                    LastPageIndexToShow = paginationResult.LastPageIndexToShow,
                    NextIsDisabled = paginationResult.NextIsDisabled,
                    PagesCount = paginationResult.PagesCount,
                    PrevIsDisabled = paginationResult.PrevIsDiabled,
                    PeriodIsSearched = periodIsSearched,
                    SpecificDateIsSearched = specificDateIsSearched,
                    RequestedPageIndex = request.PageIndex,
                    ItemsInPageCount = request.ItemsInPageCount,
                    BaseWorkTime = request.BaseWorkTime
                }

            };
        }

    }

    public class BaseWorkTimeFilter : IFilterService<WorkerToShowInListDto>
    {
        public int BaseWorkHour { get; set; }

        public BaseWorkTimeFilter(int baseWorkHour)
        {
            BaseWorkHour = baseWorkHour;
        }

        public IEnumerable<WorkerToShowInListDto> Execute(IEnumerable<WorkerToShowInListDto> source)
        {
            if (this.BaseWorkHour > 0)
                return source.Where(x => x.WorkHour > this.BaseWorkHour);
            return source;
        }
    }

    public class PeriodFilter : IFilterService<Report>
    {
        public DateTime? StartPeriod { get; set; }
        public DateTime? FinishPeriod { get; set; }

        public PeriodFilter(DateTime? startPeriod, DateTime? finishPeriod)
        {
            StartPeriod = startPeriod;
            FinishPeriod = finishPeriod;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            if (this.StartPeriod == null)
                return source;
            return source.Where(p => p.Date >= this.StartPeriod && p.Date <= this.FinishPeriod);

        }
    }
    public class SpecificDateFilter : IFilterService<Report>
    {
        public DateTime? SearchKeyDate { get; set; }

        public SpecificDateFilter(DateTime? searchKeyDate)
        {
            SearchKeyDate = searchKeyDate;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            if (this.SearchKeyDate == null)
                return source;
            return source.Where(p => p.Date == this.SearchKeyDate);
        }
    }

    public class WorkersTotalWorkTimeResultDto
    {
        public List<WorkerToShowInListDto> WorkersList { get; set; }
        public int RequestedPageIndex { get; set; }
        public string RequestedSearchKeyDate { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public int ItemsInPageCount { get; set; }
        public string StartPeriod { get; set; }
        public string FinishPeriod { get; set; }
        public bool PeriodIsSearched { get; set; } = false;
        public bool SpecificDateIsSearched { get; set; } = false;
        public int BaseWorkTime { get; set; }
    }

    public class WorkerToShowInListDto
    {
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string UserId { get; set; }
        public string WorkTime { get; set; }
        public string RemoteWorkTime { get; set; }
        public string NoneRemoteWorkTime { get; set; }
        public int WorkHour { get; set; }
    }

    public class WorkersTotalWorkTimeRequestDto
    {
        public string StartPeriod { get; set; }
        public string FinishPeriod { get; set; }
        public string SearchKeyDate { get; set; }
        public int PageIndex { get; set; }
        public int ItemsInPageCount { get; set; }
        public int BaseWorkTime { get; set; } = 0;
    }
}
