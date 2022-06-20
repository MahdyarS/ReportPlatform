using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.Pagination;
using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.UtilityServices.FilterResults;
using Reports.Helpers.UtilityServices.TimeFormat;

namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
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


}
