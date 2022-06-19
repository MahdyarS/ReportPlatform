using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reports.Helpers.UtilityServices.DateConversionService;
using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.UtilityServices.FilterResults;
using Reports.Helpers.UtilityServices.TimeFormat;

namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public interface IGetUserReportsListService
    {
        ResultDto<GetUsersReportsResultDto> Execute(GetReportServiceRequestDto request);
    }

    public class GetUserReportsListService : IGetUserReportsListService
    {
        private readonly Context _context;

        public GetUserReportsListService(Context context)
        {
            _context = context;
        }

        public ResultDto<GetUsersReportsResultDto> Execute(GetReportServiceRequestDto request)
        {
            var result = new ResultDto<GetUsersReportsResultDto>(false, "")
            {
                Data = new GetUsersReportsResultDto
                {
                    UserId = request.UserId,
                    UsersFirstName = request.UsersFirstName,
                    UsersLastName = request.UsersLastName,
                    StartPeriod = request.StartPreriod,
                    FinishPeriod = request.FinishPreriod,
                    SearchKeyDate = request.SearchKeyDate,
                    PeriodName = request.PeriodName,
                    RequestedPageIndex = request.PageIndex,
                    HasRemoteReports = request.HasRemoteReports,
                    HasNoneRemoteReports = request.HasNoneRemoteReports

                }
            };
            var dateValidationResult = DateValidate(request.StartPreriod, request.FinishPreriod, request.SearchKeyDate);
            if (dateValidationResult.IsInvalidPeriod)
            {
                result.Message = "بازه وارد شده معتبر نیست!";
                return result;
            }

            result.Data.PeriodIsSearched = dateValidationResult.PeriodIsSearched;
            result.Data.SpecificDateisSearched = dateValidationResult.SpecificDateIsSearched;

            var reportFiltersList = new List<IFilterService<Report>>();
            reportFiltersList.Add(new PeriodFilter(dateValidationResult.StartPeriod, dateValidationResult.FinishPeriod));
            reportFiltersList.Add(new SpecificDateFilter(dateValidationResult.SearchKeyDate));
            reportFiltersList.Add(new RemoteOrNoneRemoteFilter(request.HasRemoteReports, request.HasNoneRemoteReports));
            reportFiltersList.Add(new UserIdFilter(request.UserId));

            var query = _context.Reports.ApplyFilters(reportFiltersList).OrderByDescending(p => p.Date);

            if (dateValidationResult.PeriodIsSearched)
            {
                var totalTicks = query.Select(p => new { WorkTimeInDay = new TimeSpan(0, p.TotalWorkedMinutes, 0) });

                double totalMinutes = 0;
                foreach (var item in totalTicks)
                {
                    totalMinutes += item.WorkTimeInDay.TotalMinutes;
                }
                int MinutestToShow = (int)totalMinutes % 60;
                int HoursToShow = (int)(totalMinutes) / 60;
                result.Data.TotalHoursWorkedInPeriod = HoursToShow.ToString() + ":" + MinutestToShow.ToString();
            }

            var paginationResult = query.Select(p => new ReportToShowDto
            {
                ReportId = p.ReportId,
                Date = p.Date.ConvertMiladiToShamsi(),
                FinishWorkTime = p.IsRemote ? "ندارد" : p.StartWorkTime!.Value.Add(new TimeSpan(0, p.TotalWorkedMinutes, 0)).ToString("hh':'mm"),
                StartWorkTime = p.IsRemote ? "ندارد" : p.StartWorkTime!.Value.ToString("hh':'mm"),
                ReportsDetail = p.ReportsDetail,
                IsRemote = p.IsRemote ? "غیرحضوری" : "حضوری",
                WorkTime = TimeFormat.TotalMinutesToTimeFormat(p.TotalWorkedMinutes),
            })
            .ToPaged(request.PageIndex, request.ItemsInPageCount);

            if (!paginationResult.Succeeded)
            {
                result.Message = paginationResult.Message;
                return result;
            }

            result.Succeeded = true;
            result.Data.ReportsList = paginationResult.RequestedPageList;
            result.Data.PrevIsDisabled = paginationResult.PrevIsDiabled;
            result.Data.NextIsDisabled = paginationResult.NextIsDisabled;
            result.Data.FirstPageIndexToShow = paginationResult.FirstPageIndexToShow;
            result.Data.LastPageIndexToShow = paginationResult.LastPageIndexToShow;
            result.Data.PagesCount = paginationResult.PagesCount;
            result.Data.RequestedPageIndex = paginationResult.RequestedPageIndex;

            return result;
        }

        private DateValidationResultDto DateValidate(string startPeriod, string finishPeriod, string searchKeyDate)
        {
            var result = new DateValidationResultDto();
            if (!String.IsNullOrEmpty(searchKeyDate))
            {
                result.SearchKeyDate = searchKeyDate.ShamsiStringToDateTime();
                result.SpecificDateIsSearched = true;
            }
            else if (!String.IsNullOrEmpty(startPeriod) && !String.IsNullOrEmpty(finishPeriod))
            {
                result.StartPeriod = startPeriod.ShamsiStringToDateTime();
                result.FinishPeriod = finishPeriod.ShamsiStringToDateTime();
                if (result.StartPeriod > result.FinishPeriod)
                    result.IsInvalidPeriod = true;
                result.PeriodIsSearched = true;
            }
            if ((!String.IsNullOrEmpty(startPeriod) && String.IsNullOrEmpty(finishPeriod)) || (String.IsNullOrEmpty(startPeriod) && !String.IsNullOrEmpty(finishPeriod)))
            {
                result.IsInvalidPeriod = true;
                result.PeriodIsSearched = true;
            }
            return result;
        }

    }

    public class DateValidationResultDto
    {
        public bool SpecificDateIsSearched { get; set; } = false;
        public bool PeriodIsSearched { get; set; } = false;
        public bool IsInvalidPeriod { get; set; } = false;
        public DateTime? StartPeriod { get; set; }
        public DateTime? FinishPeriod { get; set; }
        public DateTime? SearchKeyDate { get; set; }
    }
    public class RemoteOrNoneRemoteFilter : IFilterService<Report>
    {
        public bool HasRemoteReports { get; set; } = true;
        public bool HasNoneRemoteReports { get; set; } = true;

        public RemoteOrNoneRemoteFilter(bool hasRemoteReports, bool hasNoneRemoteReports)
        {
            HasRemoteReports = hasRemoteReports;
            HasNoneRemoteReports = hasNoneRemoteReports;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            if (!HasRemoteReports && !HasNoneRemoteReports)
                return source.Where(p => false);
            if (!HasRemoteReports && HasNoneRemoteReports)
                return source.Where(p => p.IsRemote == false);
            if (HasRemoteReports && !HasNoneRemoteReports)
                return source.Where(p => p.IsRemote);
            return source;
        }
    }
    public class UserIdFilter : IFilterService<Report>
    {
        public string UserId { get; set; }
        public UserIdFilter(string userId)
        {
            UserId = userId;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            if (!String.IsNullOrEmpty(this.UserId))
                return source.Where(p => p.UserId == this.UserId);
            return source;
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
            if (this.StartPeriod == null || this.FinishPeriod == null)
                return source;
            return source.Where(p => p.Date >= this.StartPeriod && p.Date <= this.FinishPeriod);

        }
    }
    public class GetUsersReportsResultDto
    {
        public List<ReportToShowDto>? ReportsList { get; set; }
        public string TotalHoursWorkedInPeriod { get; set; }
        public int RequestedPageIndex { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public bool PeriodIsSearched { get; set; }
        public bool SpecificDateisSearched { get; set; }
        public string SearchKeyDate { get; set; } = "";
        public string StartPeriod { get; set; } = "";
        public string FinishPeriod { get; set; } = "";
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string PeriodName { get; set; }
        public bool HasRemoteReports { get; set; }
        public bool HasNoneRemoteReports { get; set; }
    }

    public class ReportToShowDto
    {
        public int ReportId { get; set; }
        public string Date { get; set; }
        public string StartWorkTime { get; set; }
        public string FinishWorkTime { get; set; }
        public string WorkTime { get; set; }
        public string ReportsDetail { get; set; }
        public string IsRemote { get; set; }
    }

    public class GetReportServiceRequestDto
    {
        public string PeriodName { get; set; } = "";
        public string UsersFirstName { get; set; } = "";
        public string UsersLastName { get; set; } = "";
        public string SearchKeyDate { get; set; } = "";
        public string StartPreriod { get; set; } = "";
        public string FinishPreriod { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 1;
        public string UserId { get; set; }
        public bool HasRemoteReports { get; set; } = true;
        public bool HasNoneRemoteReports { get; set; } = true;
    }


}
