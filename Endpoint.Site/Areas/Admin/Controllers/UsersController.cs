using Endpoint.Site.Areas.Admin.Models.UserControllerModels;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.UserServices.RegisterUserService;
using Reports.Application.Services.UserServices.GetUsersService;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly IGetUsersService _getUserService;

        public UsersController(IRegisterUserService registerUserService, IGetUsersService getUserService)
        {
            _registerUserService = registerUserService;
            _getUserService = getUserService;
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
    }
}
