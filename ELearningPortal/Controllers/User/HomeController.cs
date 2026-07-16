using ELearningPortal.Interfaces.ISuperAdmin;
using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    [Route("User/[controller]")]
    [Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        IHomeService homeService;
        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
