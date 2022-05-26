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
            bool isEmptySearchKey = request.SearchKeyDate == "" || request.SearchKeyDate == null ? true : false;
            DateTime searchKeyDate = new DateTime();

            if (!isEmptySearchKey)
                searchKeyDate = request.SearchKeyDate.ShamsiStringToDateTime();


            bool isEmptyPeriod = request.SearchKeyStartPreriodDate == "" || request.SearchKeyDate == null ? true : false;
            DateTime StartPeriod = new DateTime();
            DateTime FinishPeriod = new DateTime();

            var user = _context.Users.SingleOrDefault(p => p.UserName == request.UserName);

            if (user == null)
                return new ResultDto<GetUsersReportsResultDto>(false, "کاربر مورد نظر یافت نشد!");

            var query = _context.Reports.Where(p => (((isEmptySearchKey && isEmptyPeriod) ||
                                                                (!isEmptySearchKey && p.Date == searchKeyDate) ||
                                                                (!isEmptyPeriod && p.Date <= FinishPeriod && p.Date >= StartPeriod))) &&
                                                                p.UserId == user.Id);

            string totalWorkHours = "";

            if (!isEmptyPeriod)
            {
                StartPeriod = request.SearchKeyStartPreriodDate.ShamsiStringToDateTime();
                if (request.SearchKeyFinishPreriodDate == "")
                    FinishPeriod = DateTime.Now;

                else
                    FinishPeriod = request.SearchKeyFinishPreriodDate.ShamsiStringToDateTime();

                if (FinishPeriod < StartPeriod)
                    return new ResultDto<GetUsersReportsResultDto>(false, "بازه داده شده معتبر نیست!")
                    {
                        Data = new GetUsersReportsResultDto
                        {
                            UserName = request.UserName,
                            UsersFirstName = user.FirstName,
                            UsersLastName = user.LastName
                        }
                    };

                var totalTicks = query.Select(p => new { WorkTimeInDay = p.FinishWorkTime.Subtract(p.StartWorkTime) }).ToList().Sum(p => p.WorkTimeInDay.Ticks);
                totalWorkHours = new TimeSpan(totalTicks).ToString("hh':'mm");
            }


            var paginationResult = query.Select(p => new ReportToShowDto
            {
                ReportId = p.ReportId,
                Date = p.Date.ConvertMiladiToShamsi("yyyy/MM/dd"),
                FinishWorkTime = p.FinishWorkTime.ToString("hh':'mm"),
                StartWorkTime = p.StartWorkTime.ToString("hh':'mm"),
                ReportsDetail = p.ReportsDetail,
                IsRemote = p.IsRemote ? "غیرحضوری" : "حضوری",
                WorkTime = p.FinishWorkTime.Subtract(p.StartWorkTime).ToString("hh':'mm"),
            }).ToPaged(request.PageIndex, request.ItemsInPageCount);



            if (!paginationResult.Succeeded)
            {
                if (paginationResult.PagesCount == 0)
                    return new ResultDto<GetUsersReportsResultDto>(false, "گزارشی در تاریخ مورد نظر ثبت نشده است!")
                    {
                        Data = new GetUsersReportsResultDto
                        {
                            UserName = request.UserName,
                            UsersFirstName = user.FirstName,
                            UsersLastName = user.LastName
                        }
                    };

                return new ResultDto<GetUsersReportsResultDto>(false, paginationResult.Message)
                {
                    Data = new GetUsersReportsResultDto
                    {
                        UserName = request.UserName,
                        UsersFirstName = user.FirstName,
                        UsersLastName = user.LastName
                    }
                };

            }

            var result = new ResultDto<GetUsersReportsResultDto>(true, paginationResult.Message)
            {
                Data = new GetUsersReportsResultDto
                {
                    FirstPageIndexToShow = paginationResult.FirstPageIndexToShow,
                    LastPageIndexToShow = paginationResult.LastPageIndexToShow,
                    NextIsDisabled = paginationResult.NextIsDisabled,
                    PagesCount = paginationResult.PagesCount,
                    PrevIsDisabled = paginationResult.PrevIsDiabled,
                    RequestedPageIndex = request.PageIndex,
                    RequestedSearchKeyDate = request.SearchKeyDate,
                    ReportsList = paginationResult.RequestedPageList,
                    PeriodIsSearched = !isEmptyPeriod,
                    SpecificDateisSearched = !isEmptySearchKey,
                    SearchKeyStartPreriodDate = request.SearchKeyStartPreriodDate,
                    SearchKeyFinishPreriodDate = request.SearchKeyFinishPreriodDate,
                    TotalHoursWorkedInPeriod = totalWorkHours,
                    UserName = request.UserName,
                    UsersFirstName = user.FirstName,
                    UsersLastName = user.LastName,
                    SearchKeyStartPreriodDateTime = StartPeriod.Date,
                    SearchKeyFinishPreriodDateTime = FinishPeriod.Date
                }
            };

            return result;
        }
    }

    public class GetUsersReportsResultDto
    {
        public List<ReportToShowDto>? ReportsList { get; set; }
        public string TotalHoursWorkedInPeriod { get; set; }
        public int RequestedPageIndex { get; set; }
        public string RequestedSearchKeyDate { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public bool PeriodIsSearched { get; set; }
        public bool SpecificDateisSearched { get; set; }
        public string SearchKeyStartPreriodDate { get; set; }
        public string SearchKeyFinishPreriodDate { get; set; }
        public DateTime SearchKeyStartPreriodDateTime { get; set; }
        public DateTime SearchKeyFinishPreriodDateTime { get; set; }
        public string UserName { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
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
        public string SearchKeyDate { get; set; } = "";
        public string SearchKeyStartPreriodDate { get; set; } = "";
        public string SearchKeyFinishPreriodDate { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 1;
        public string UserName { get; set; }
    }


}
