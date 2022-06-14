using Endpoint.Site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.MutitaskServices.UserPanelHomePageService;
using System.Diagnostics;
using System.Security.Claims;

namespace Endpoint.Site.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserPanelHomePageService _userPanelHomePageService;

        public HomeController(ILogger<HomeController> logger,
                              IUserPanelHomePageService userPanelHomePageService)
        {
            _logger = logger;
            _userPanelHomePageService = userPanelHomePageService;
        }

        public IActionResult Index()
        {
            return View(_userPanelHomePageService.Execute(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}