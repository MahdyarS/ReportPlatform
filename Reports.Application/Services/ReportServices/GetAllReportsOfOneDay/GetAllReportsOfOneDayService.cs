using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Reports;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Helpers.UtilityServices.FilterResults;
using Reports.Helpers.UtilityServices.Pagination;
using Reports.Helpers.UtilityServices.TimeFormat;

namespace Reports.Application.Services.ReportServices.GetAllReportsOfOneDay
{
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
                    SearchKeyDate = (!String.IsNullOrEmpty(request.Date))? request.Date : DateTime.Now.ConvertMiladiToShamsi(),
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


}
