using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Services.MutitaskServices.AdminPanelHomePageService;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IAdminPanelHomePageService _adminPanelHomePageService;

        public HomeController(IAdminPanelHomePageService adminPanelHomePageService)
        {
            _adminPanelHomePageService = adminPanelHomePageService;
        }

        public IActionResult Index()
        {
            return View(_adminPanelHomePageService.Execute());
        }
    }
}
