using Endpoint.Site.Models.ReportsControllerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.PeriodServices.AddNewPeriodService;
using Reports.Application.Services.ReportServices.AddNewReportService;
using Reports.Application.Services.ReportServices.DeleteReportService;
using Reports.Application.Services.ReportServices.EditReportService;
using Reports.Application.Services.PeriodServices.GetPeriodsListService;
using Reports.Application.Services.ReportServices.GetReportToEditById;
using Reports.Application.Services.ReportServices.GetUserReportsListService;
using Reports.Helpers.UtilityServices.DateConversionService;
using Reports.Application.Services.PeriodServices.DeletePeriodService;
using Reports.Application.Services.PeriodServices.GetPeriodByIdService;
using Reports.Application.Services.PeriodServices.EditPeriodService;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Reports.Helpers.Dtos.ResultDto;

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
        private readonly IAddNewPeriodService _addNewPeriodService;
        private readonly IGetPeriodsListService _getPeriodsListService;
        private readonly IDeletePeriodService _deletePeriodService;
        private readonly IGetPeriodByIdService _getPeriodByIdService;
        private readonly IEditPeriodService _editPeriodService;

        public ReportsController(IAddNewReportService addNewReportService,
                                 IGetUserReportsListService getUserReportsListService,
                                 IGetReportToEditById getReportToEditById,
                                 IEditReportService editReportService,
                                 IDeleteReportService deleteReportService,
                                 IAddNewPeriodService addNewPeriodService,
                                 IGetPeriodsListService getPeriodsListService,
                                 IDeletePeriodService deletePeriodService,
                                 IGetPeriodByIdService getPeriodByIdService,
                                 IEditPeriodService editPeriodService)
        {
            _addNewReportService = addNewReportService;
            _getUserReportsListService = getUserReportsListService;
            _getReportToEditById = getReportToEditById;
            _editReportService = editReportService;
            _deleteReportService = deleteReportService;
            _addNewPeriodService = addNewPeriodService;
            _getPeriodsListService = getPeriodsListService;
            _deletePeriodService = deletePeriodService;
            _getPeriodByIdService = getPeriodByIdService;
            _editPeriodService = editPeriodService;
        }

        [HttpGet]
        public IActionResult AddNewReport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewReport(ReportToAddViewModel report)
        {
            if (!ModelState.IsValid)
            {
                return View(report);
            }

            var result = await _addNewReportService.Execute(new ReportToAddRequestDto()
            {
                BeginningTime = report.BeginningTime,
                Date = report.Date,/////////////////////////////
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
            if (!ModelState.IsValid)
            {
                string message = ModelState.Values.Where(p => p.Errors.Count > 0).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
                return View(new ResultDto<GetUsersReportsResultDto>(false, message));
            }

            var result = _getUserReportsListService.Execute(new GetReportServiceRequestDto()
            {
                PeriodName = request.PeriodName,
                ItemsInPageCount = request.ItemsInPageCount,
                PageIndex = request.PageIndex,
                SearchKeyDate = request.SearchKeyDate,
                StartPreriod = request.StartPeriod,
                FinishPreriod = request.FinishPeriod,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                HasNoneRemoteReports = request.HasNoneRemoteReports == "False" ? false : true,
                HasRemoteReports = request.HasRemoteReports == "False" ? false : true,
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
            //ViewBag.Error = "";
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
            if (!ModelState.IsValid)
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
            return Json(_deleteReportService.Execute(reportId, User.Identity.Name));
        }

        [HttpGet]
        public IActionResult AddPeriod([RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))",ErrorMessage ="تاریخ وارد شده صحیح نیست!")]
                                        string start,
                                        [RegularExpression(@"^(\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01]))",ErrorMessage ="تاریخ وارد شده صحیح نیست!")]
                                        string finish)
        {
            return View(new AddPeriodViewModel
            {
                StartPeriodDate = start,/////////////////////////////
                FinishPeriodDate = finish/////////////////////////////
            });
        }

        [HttpPost]
        public IActionResult AddPeriod(AddPeriodViewModel request)
        {
            if(!ModelState.IsValid)
                return View(request);

            var result = _addNewPeriodService.Execute(new NewPeriodToAddRequestDto
            {
                StartPeriodDate = request.StartPeriodDate,
                FinishPeriodDate = request.FinishPeriodDate,
                PeriodDescription = request.PeriodDescription,
                PeriodName = request.PeriodName,
                UserName = User.Identity.Name
            });

            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View(request);
            }

            TempData["Message"] = result.Message;

            return RedirectToAction("PeriodsList");
        }


        public IActionResult PeriodsList(PeriodsListRequestModel request)
        {

            var result = _getPeriodsListService.Execute(new GetPeriodListRequestDto
            {
                SearchKey = request.SearchKey,
                PageIndex = request.PageIndex,
                ItemsInPageCount = request.ItemsInPageCount,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });

            return View(result);
        }


        [HttpGet]
        public IActionResult EditPeriod(int PeriodId)
        {
            var result = _getPeriodByIdService.Execute(PeriodId, User.Identity.Name);

            if (!result.Succeeded)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("PeriodsList");
            }

            return View(new EditPeriodViewModel
            {
                PeriodId = PeriodId,
                StartPeriod = result.Data.StartPeriodDate,
                FinishPeriod = result.Data.FinishPeriodDate,
                PeriodDescription = result.Data.PeriodDescription,
                PeriodName = result.Data.PeriodName
            });
        }

        [HttpPost]
        public IActionResult EditPeriod(EditPeriodViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = _editPeriodService.Execute(new EditPeriodRequestDto
            {
                PeriodId = request.PeriodId,
                FinishPeriod = request.FinishPeriod,
                PeriodDescription = request.PeriodDescription,
                PeriodName = request.PeriodName,
                StartPeriod = request.StartPeriod,
                UserName = User.Identity.Name
            });

            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View(request);
            }

            TempData["Message"] = result.Message;

            return RedirectToAction("PeriodsList");
        }

        public IActionResult DeletePeriod(int PeriodId)
        {
            return Json(_deletePeriodService.Execute(PeriodId, User.Identity.Name));
        }
    }
}
