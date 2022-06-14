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
            DateTime requestedDate;
            if (String.IsNullOrEmpty(request.Date))
            {
                requestedDate = DateTime.Now;
                request.Date = requestedDate.ConvertMiladiToShamsi();
            }
            else
                requestedDate = request.Date.ShamsiStringToDateTime();

            var paginationResult = _context.Reports.Include(p => p.User).Where(p => p.Date == requestedDate)
                .Select(p => new ReportToShowDto
                {
                    UsersFirstName = p.User.FirstName,
                    UsersLastName = p.User.LastName,
                    StartWorkTime = p.StartWorkTime.ToString("hh':'mm"),
                    FinishWorkTime = p.FinishWorkTime.ToString("hh':'mm"),
                    ReportsDescription = p.ReportsDetail,
                    TotalWorkTime = p.FinishWorkTime.Subtract(p.StartWorkTime).ToString("hh':'mm")
                }).ToPaged(request.RequestedPageIndex, request.ItemsInPageCount);

            if (!paginationResult.Succeeded)
                return new ResultDto<GetAllReportsOfOneDayResultDto>(false, paginationResult.Message)
                {
                    Data = new GetAllReportsOfOneDayResultDto
                    {
                        SearchKeyDate = request.Date
                    }
                };


            return new ResultDto<GetAllReportsOfOneDayResultDto>(true, "")
            {
                Data = new GetAllReportsOfOneDayResultDto
                {
                    ReportsList = paginationResult.RequestedPageList,
                    FirstPageIndexToShow = paginationResult.FirstPageIndexToShow,
                    LastPageIndexToShow = paginationResult.LastPageIndexToShow,
                    NextIsDisabled = paginationResult.NextIsDisabled,
                    PagesCount = paginationResult.PagesCount,
                    PrevIsDisabled = paginationResult.PrevIsDiabled,
                    SearchKeyDate = request.Date,
                    RequestedPageIndex = request.RequestedPageIndex,
                    RequestedItemsInPageCount = request.ItemsInPageCount
                }
            };

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
    }

    public class ReportToShowDto
    {
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string StartWorkTime { get; set; }
        public string FinishWorkTime { get; set; }
        public string TotalWorkTime { get; set; }
        public string ReportsDescription { get; set; }
    }

    public class GetReportsOfOneDayRequestDto
    {
        public string Date { get; set; } = "";
        public int RequestedPageIndex { get; set; }
        public int ItemsInPageCount { get; set; } = 1;
    }


}
