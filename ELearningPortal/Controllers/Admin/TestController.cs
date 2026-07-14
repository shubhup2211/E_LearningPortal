using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.Admin
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
