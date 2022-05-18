using Endpoint.Site.Models.AccountControllerModels;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.UserServices.LoginService;

namespace Endpoint.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;

        public AccountController(ILoginService loginService)
        {
            _loginService = loginService;
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
        public IActionResult Logout()
        {
            return View();
        }
    }
}
