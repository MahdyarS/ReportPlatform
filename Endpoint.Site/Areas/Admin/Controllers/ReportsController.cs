using Endpoint.Site.Areas.Admin.Models.ReportsControllerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.PeriodServices.GetPeriodsListService;
using Reports.Application.Services.ReportServices.GetAllReportsOfOneDay;
using Reports.Application.Services.ReportServices.GetUserReportsListService;
using Reports.Application.Services.ReportServices.GetWorkersTotalWorkTimeListService;
using Reports.Application.Services.ReportServices.GetWorkTimeChart;
using Reports.Helpers.Dtos.ResultDto;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly IGetUserReportsListService _getUserReportsListService;
        private readonly IGetPeriodsListService _getPeriodsListService;
        private readonly IGetAllReportsOfOneDayService _getAllReportsOfOneDayService;
        private readonly IGetWorkTimeChart _getWorkTimeChart;
        private readonly IGetWorkersTotalWorkTimeListService _getWorkersTotalWorkTimeListService;

        public ReportsController(IGetUserReportsListService getUserReportsListService,
                                 IGetPeriodsListService getPeriodsListService,
                                 IGetAllReportsOfOneDayService getAllReportsOfOneDayService,
                                 IGetWorkTimeChart getWorkTimeChart,
                                 IGetWorkersTotalWorkTimeListService getWorkersTotalWorkTimeListService)
        {
            _getUserReportsListService = getUserReportsListService;
            _getPeriodsListService = getPeriodsListService;
            _getAllReportsOfOneDayService = getAllReportsOfOneDayService;
            _getWorkTimeChart = getWorkTimeChart;
            _getWorkersTotalWorkTimeListService = getWorkersTotalWorkTimeListService;
        }

        public IActionResult GetOneUsersReports(GetOneUsersReportsViewModelRequest request)
        {
            if (!ModelState.IsValid)
            {
                string message = ModelState.Values.Where(p => p.Errors.Count > 0).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
                return View(new ResultDto<GetUsersReportsResultDto>(false, message)
                {
                    Data = new GetUsersReportsResultDto
                    {
                        UserId = request.UserId,
                        UsersFirstName = request.UsersFirstName,
                        UsersLastName = request.UsersLastName,
                    }
                });
            }

            var result = _getUserReportsListService.Execute(new GetReportServiceRequestDto
            {
                ItemsInPageCount = request.ItemsInPageCount,
                PageIndex = request.PageIndex,
                SearchKeyDate = request.SearchKeyDate,
                StartPreriod = request.StartPeriod,
                FinishPreriod = request.FinishPeriod,
                UserId = request.UserId,
                UsersFirstName = request.UsersFirstName,
                UsersLastName = request.UsersLastName,
                PeriodName = request.PeriodName,
                HasNoneRemoteReports = request.HasNoneRemoteReports == "False" ? false : true,
                HasRemoteReports = request.HasRemoteReports == "False" ? false : true,
            });
            return View(result);
        }

        public IActionResult ShowUserPeriods(ShowUserPeriodsRequestModel request)
        {
            var result = _getPeriodsListService.Execute(new GetPeriodListRequestDto
            {
                UserId = request.UserId,
                SearchKey = request.SearchKey,
                UsersFirstName = request.UsersFirstName,
                UsersLastName = request.UsersLastName,
                ItemsInPageCount = request.ItemsInPageCount,
                PageIndex = request.PageIndex
            });


            return View(result);
        }

        public IActionResult GetReportsOfEachDay(GetReportsOfEachDayRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = ModelState.Values.Where(p => p.Errors.Count > 0).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
                return View(new ResultDto<GetAllReportsOfOneDayResultDto>(false, message));
            }

            var result = _getAllReportsOfOneDayService.Execute(new GetReportsOfOneDayRequestDto
            {
                Date = request.Date,
                ItemsInPageCount = request.ItemsInPageCount,
                RequestedPageIndex = request.PageIndex
            });
            return View(result);
        }

        public IActionResult AllWorkersWorkTimeChart(AllWorkersWorkTimeChartRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = ModelState.Values.Where(p => p.Errors.Count > 0).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
                return View(new ResultDto<GetWorkTimeChartResultDto>(false, message));
            }


            var result = _getWorkTimeChart.Execute(new GetWorkTimeChartRequestDto
            {
                StartDate = request.StartDate,
                FinishDate = request.FinishDate
            });

            return View(result);
        }

        public IActionResult WorkersWorkTime(WorkersWorkTimeRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = ModelState.Values.Where(p => p.Errors.Count > 0).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
                return View(new ResultDto<WorkersTotalWorkTimeResultDto>(false, message));
            }
            
            var result = _getWorkersTotalWorkTimeListService.Execute(new WorkersTotalWorkTimeRequestDto()
            {
                ItemsInPageCount= request.ItemsInPageCount,
                BaseWorkTime = request.BaseWorkTime,
                FinishPeriod = request.FinishPeriod,
                StartPeriod = request.StartPeriod,
                SearchKeyDate = request.SearchKeyDate,
                PageIndex = request.PageIndex
            });

            return View(result);
        }
    }
}
