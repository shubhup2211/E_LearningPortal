using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    public class LearningPathController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
