using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.FilterResults;
using Reports.Helpers.UtilityServices.Pagination;
using Reports.Helpers.UtilityServices.TimeFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.GetAllReportsOfOneDay
{
    public interface IGetAllReportsOfOneDayService
    {
        ResultDto<GetAllReportsOfOneDayResultDto> Execute(GetReportsOfOneDayRequestDto request);
    }
    public class GetAllReportsOfOneDayService : IGetAllReportsOfOneDayService
    {
        private readonly Context _context;

        public GetAllReportsOfOneDayService(Context context)
        {
            _context = context;
        }

        public ResultDto<GetAllReportsOfOneDayResultDto> Execute(GetReportsOfOneDayRequestDto request)
        {
            var result = new ResultDto<GetAllReportsOfOneDayResultDto>(false, "")
            {
                Data = new GetAllReportsOfOneDayResultDto
                {
                    SearchKeyDate = request.Date,
                    RequestedPageIndex = request.RequestedPageIndex,
                    RequestedItemsInPageCount = request.ItemsInPageCount,
                    HasNoneRemoteReports = request.HasNoneRemoteReports,
                    HasRemoteReports = request.HasRemoteReports
                }
            };

            DateTime requestedDate;
            if (String.IsNullOrEmpty(request.Date))
            {
                requestedDate = DateTime.Now;
                request.Date = requestedDate.ConvertMiladiToShamsi();
            }
            else
                requestedDate = request.Date.ShamsiStringToDateTime();

            var reportFiltersList = new List<IFilterService<Report>>();
            reportFiltersList.Add(new DateFilter(requestedDate));
            reportFiltersList.Add(new RemoteOrNoneRemoteFilter(request.HasRemoteReports, request.HasNoneRemoteReports));


            var query = _context.Reports.Include(p => p.User).ApplyFilters(reportFiltersList);


            var paginationResult = query.Select(p => new
                {
                    p.User.FirstName,
                    p.User.LastName,
                    p.StartWorkTime,
                    p.TotalWorkedMinutes,
                    p.ReportsDetail,
                    p.IsRemote
                }).ToPaged(request.RequestedPageIndex, request.ItemsInPageCount);

                
                 

            if (!paginationResult.Succeeded)
            {
                result.Message = paginationResult.Message;
                return result;
            }
            
            result.Succeeded = true;
            result.Data.ReportsList = paginationResult.RequestedPageList.Select(p => new ReportToShowDto
            {
                UsersFirstName = p.FirstName,
                UsersLastName = p.LastName,
                StartWorkTime = p.IsRemote? "ندارد": p.StartWorkTime!.Value.ToString("hh':'mm"),
                FinishWorkTime = p.IsRemote ? "ندارد" : p.StartWorkTime!.Value.Add(new TimeSpan(0,p.TotalWorkedMinutes,0)).ToString("hh':'mm"),
                ReportsDescription = p.ReportsDetail,
                TotalWorkTime = TimeFormat.TotalMinutesToTimeFormat(p.TotalWorkedMinutes),
                IsRemote = p.IsRemote ? "غیرحضوری" : "حضوری",
            }).ToList();
            result.Data.RequestedPageIndex = paginationResult.RequestedPageIndex;
            result.Data.FirstPageIndexToShow = paginationResult.FirstPageIndexToShow;
            result.Data.LastPageIndexToShow = paginationResult.LastPageIndexToShow;
            result.Data.NextIsDisabled = paginationResult.NextIsDisabled;
            result.Data.PagesCount = paginationResult.PagesCount;
            result.Data.PrevIsDisabled = paginationResult.PrevIsDiabled;

            return result;

        }
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
    public class DateFilter : IFilterService<Report>
    {
        public DateTime Date { get; set; }

        public DateFilter(DateTime date)
        {
            Date = date;
        }

        public IEnumerable<Report> Execute(IEnumerable<Report> source)
        {
            return source.Where(p => p.Date.Date == this.Date.Date);
        }
    }

    public class GetAllReportsOfOneDayResultDto
    {
        public List<ReportToShowDto> ReportsList { get; set; }
        public int RequestedPageIndex { get; set; }
        public int RequestedItemsInPageCount { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }
        public bool PrevIsDisabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public string SearchKeyDate { get; set; }
        public bool HasRemoteReports { get; set; }
        public bool HasNoneRemoteReports { get; set; }
    }

    public class ReportToShowDto
    {
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string StartWorkTime { get; set; }
        public string FinishWorkTime { get; set; }
        public string IsRemote { get; set; }
        public string TotalWorkTime { get; set; }
        public string ReportsDescription { get; set; }
    }

    public class GetReportsOfOneDayRequestDto
    {
        public string Date { get; set; } = "";
        public bool HasRemoteReports { get; set; } = true;
        public bool HasNoneRemoteReports { get; set; } = true;
        public int RequestedPageIndex { get; set; }
        public int ItemsInPageCount { get; set; } = 1;
    }


}
