using Endpoint.Site.Models.AccountControllerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.UserServices.LoginService;
using Reports.Application.Services.UserServices.LogoutService;
using Reports.Application.Services.UserServices.GetUserInformationByIdService;
using System.Security.Claims;

namespace Endpoint.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ILogoutService _logoutService;
        private readonly IGetUserInformationByIdService _getUserInformationService;

        public AccountController(ILoginService loginService,
                                 ILogoutService logoutService,
                                 IGetUserInformationByIdService getUserInformationService)
        {
            _loginService = loginService;
            _logoutService = logoutService;
            _getUserInformationService = getUserInformationService;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Error = "اطلاعات وارد شده معتبر نیست!";
                return View(request);
            }
            var result = await _loginService.Execute(new LoginServiceRequestDto 
            {
                NationalCode = request.NationalCode,
                Password = request.Password,
                IsPersistent = request.IsPersistent 
            });
            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View(request);
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _logoutService.Execute();
            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        [Route("[action]")]
        public IActionResult Profile()
        {
            return View(_getUserInformationService.Execute(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
