using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    public class MyCourseController : Controller
    {
        IMyCourseService myCouService;
        public MyCourseController(IMyCourseService myCouService)
        {
            this.myCouService = myCouService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
