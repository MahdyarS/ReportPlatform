using Endpoint.Site.Areas.Admin.Models.UserControllerModels;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.UserServices.RegisterUserService;
using Reports.Application.Services.UserServices.GetUsersService;
using Microsoft.AspNetCore.Authorization;
using Reports.Application.Services.UserServices.GetUserInformationByIdService;
using Reports.Application.Services.UserServices.EditUserService;
using Reports.Application.Services.UserServices.DeleteUserService;
using Reports.Application.Services.UserServices.ToggleUsersStatusService;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly IGetUsersService _getUserService;
        private readonly IGetUserInformationByIdService _getUserInformationByIdService;
        private readonly IEditUserService _editUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IToggleUsersStatusService _toggleUsersStatusService;

        public UsersController(IRegisterUserService registerUserService,
                               IGetUsersService getUserService,
                               IGetUserInformationByIdService getUserInformationByIdService,
                               IEditUserService editUserService,
                               IDeleteUserService deleteUserService,
                               IToggleUsersStatusService toggleUsersStatusService)
        {
            _registerUserService = registerUserService;
            _getUserService = getUserService;
            _getUserInformationByIdService = getUserInformationByIdService;
            _editUserService = editUserService;
            _deleteUserService = deleteUserService;
            _toggleUsersStatusService = toggleUsersStatusService;
        }

        public IActionResult UsersList(UsersListRequestViewModel request)
        {

            return View(_getUserService.Execute(new GetUsersServiceRequestDto
            {
                ItemsInPageCount = request.ItemsInPageCount,
                PageIndex = request.PageIndex,
                SearchKey = request.SearchKey
            }));
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "اطلاعات وارد شده معتبر نیست!";
                return View(request);
            }

            var result = await _registerUserService.Execute(new RegisterUserRequestDto()
            {
                Email = request.Email,
                FName = request.FName,
                LName = request.LName,
                PhoneNumber = request.PhoneNumber,
                Position = request.Position,
                NationalCode = request.NationalCode,
                Password = request.Password
            });

            if (result.Succeeded)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction("UsersList");
            }

            ViewBag.Error = result.Message;
            return View(request);
        }

        public IActionResult UserInformation(string UserId)
        {
            var result = _getUserInformationByIdService.Execute(UserId);
            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View();
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult EditUserInformation(string UserId)
        {
            var result = _getUserInformationByIdService.Execute(UserId);
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("UsersList");
            }
            return View(new UserToEditViewModel
            {
                UserId = UserId,
                Email = result.Data.Email,
                FName = result.Data.FName,
                LName = result.Data.LName,
                NationalCode = result.Data.NationalCode,
                PhoneNumber = result.Data.PhoneNumber,
                Position = result.Data.Position
            });
        }

        [HttpPost]
        public IActionResult EditUserInformation(UserToEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "اطلاعات وارد شده معتبر نیست!";
                return View(request);
            }

            var result = _editUserService.Execute(new UserToEditModelDto
            {
                UserId = request.UserId,
                Email = request.Email,
                FName = request.FName,
                LName = request.LName,
                NationalCode = request.NationalCode,
                PhoneNumber = request.PhoneNumber,
                Position = request.Position
            });

            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View(request);
            }

            TempData["Message"] = result.Message;

            return RedirectToAction("UsersList");
        }

        public IActionResult DeleteUser(string userId)
        {
            return Json(_deleteUserService.Execute(userId));
        }

        public IActionResult ToggleUsersStatus(string userId)
        {
            return Json(_toggleUsersStatusService.Execute(userId));
        }
    }
}
