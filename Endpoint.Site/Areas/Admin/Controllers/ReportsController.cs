using Endpoint.Site.Areas.Admin.Models.ReportsControllerModels;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.ReportServices.GetUserReportsListService;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly IGetUserReportsListService _getUserReportsListService;

        public ReportsController(IGetUserReportsListService getUserReportsListService)
        {
            _getUserReportsListService = getUserReportsListService;
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
                UserName = request.UserName
            });
            return View(result);
        }
    }
}
