using Endpoint.Site.Areas.Admin.Models.ReportsControllerModels;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.PeriodServices.GetPeriodsListService;
using Reports.Application.Services.ReportServices.GetUserReportsListService;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly IGetUserReportsListService _getUserReportsListService;
        private readonly IGetPeriodsListService _getPeriodsListService;

        public ReportsController(IGetUserReportsListService getUserReportsListService,
                                 IGetPeriodsListService getPeriodsListService)
        {
            _getUserReportsListService = getUserReportsListService;
            _getPeriodsListService = getPeriodsListService;
        }

        public IActionResult GetOneUsersReports(GetOneUsersReportsViewModelRequest request)
        {
            var result = _getUserReportsListService.Execute(new GetReportServiceRequestDto
            {
                ItemsInPageCount = request.ItemsInPageCount,
                PageIndex = request.PageIndex,
                SearchKeyDate = request.SearchKeyDate,
                SearchKeyFinishPreriodDate = request.SearchKeyFinishPreriodDate,
                SearchKeyStartPreriodDate = request.SearchKeyStartPreriodDate,
                UserId = request.UserId,
                UsersFirstName = request.UsersFirstName,
                UsersLastName = request.UsersLastName,
                PeriodName = request.PeriodName
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
    }
}
