using Endpoint.Site.Models.ReportsControllerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.ReportServices.AddNewReportService;
using Reports.Application.Services.ReportServices.DeleteReportService;
using Reports.Application.Services.ReportServices.EditReportService;
using Reports.Application.Services.ReportServices.GetReportToEditById;
using Reports.Application.Services.ReportServices.GetUserReportsListService;

namespace Endpoint.Site.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {

        private readonly IAddNewReportService _addNewReportService;
        private readonly IGetUserReportsListService _getUserReportsListService;
        private readonly IEditReportService _editReportService;
        private readonly IGetReportToEditById _getReportToEditById;
        private readonly IDeleteReportService _deleteReportService;

        public ReportsController(IAddNewReportService addNewReportService,
                                 IGetUserReportsListService getUserReportsListService,
                                 IGetReportToEditById getReportToEditById,
                                 IEditReportService editReportService,
                                 IDeleteReportService deleteReportService)
        {
            _addNewReportService = addNewReportService;
            _getUserReportsListService = getUserReportsListService;
            _getReportToEditById = getReportToEditById;
            _editReportService = editReportService;
            _deleteReportService = deleteReportService;
        }

        [HttpGet]
        public IActionResult AddNewReport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewReport(ReportToAddViewModel report)
        {
            var result = await _addNewReportService.Execute(new ReportToAddRequestDto()
            {
                BeginningTime = report.BeginningTime,
                Date = report.Date,
                FinishTime = report.FinishTime,
                ReportsDetail = report.ReportsDetail,
                IsRemote = report.IsRemote,
                UserName = User.Identity.Name
            });

            if (result.Succeeded)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction("GetReports");
            }

            ViewBag.Error = result.Message;

            return View(report);
        }

        public IActionResult GetReports(GetReportsReportViewModelViewModel request)
        {
            var result = _getUserReportsListService.Execute(new GetReportServiceRequestDto()
            {
                ItemsInPageCount = request.ItemsInPageCount,
                PageIndex = request.PageIndex,
                SearchKeyDate = request.SearchKeyDate,
                SearchKeyFinishPreriodDate = request.SearchKeyFinishPreriodDate,
                SearchKeyStartPreriodDate = request.SearchKeyStartPreriodDate,
                UserName = User.Identity.Name
            });

            return View(result);
        }

        [HttpGet]
        public IActionResult EditReport(int ReportId)
        {
            var result = _getReportToEditById.Execute(ReportId, User.Identity.Name);

            if (!result.Succeeded)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("GetReports");
            }

            return View(new EditReportViewModel
            {
                BeginningTime = result.Data.BeginningTime,
                FinishTime = result.Data.FinishTime,
                Date = result.Data.Date,
                IsRemote = result.Data.IsRemote,
                ReportsDetail = result.Data.ReportsDetail
            });
        }

        [HttpPost]
        public IActionResult EditReport(EditReportViewModel request)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Error = "اطلاعات وارد شده معتبر نیست!";
                return View(request);
            }
            var result = _editReportService.Execute(new EditReportRequest
            {
                ReportsDetail = request.ReportsDetail,
                IsRemote = request.IsRemote,
                BeginningTime = request.BeginningTime,
                FinishTime = request.FinishTime,
                ReportId = request.ReportId,
                UserName = User.Identity.Name
            });

            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View(request);
            }

            TempData["Message"] = result.Message;

            return RedirectToAction("GetReports");
        }

        public IActionResult DeleteReport(int reportId)
        {
            return Json(_deleteReportService.Execute(reportId,User.Identity.Name));
        }
    }
}
