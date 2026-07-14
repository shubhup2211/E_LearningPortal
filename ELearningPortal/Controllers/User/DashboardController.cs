using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
