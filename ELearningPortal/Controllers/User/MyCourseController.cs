using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    [Authorize(Roles = "Student")]
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

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
