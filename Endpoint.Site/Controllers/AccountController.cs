using Endpoint.Site.Models.AccountControllerModels;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.UserServices.LoginService;
using Reports.Application.Services.UserServices.LogoutService;

namespace Endpoint.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ILogoutService _logoutService;

        public AccountController(ILoginService loginService, ILogoutService logoutService)
        {
            _loginService = loginService;
            _logoutService = logoutService;
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
                UserName = request.UserName,
                Password = request.Password,
                IsPersistent = request.IsPersistent 
            });
            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View(result);
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _logoutService.Execute();
            return RedirectToAction("Index", "Home");
        }
    }
}
