using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Services.Admin;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.Admin
{
    public class UserController : Controller
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {
            var udata = userService.fetchUsers();
            return View(udata);
        }

        public IActionResult AdminUser()
        {
            var udata = userService.fetchUsers();
            return View(udata);
        }

        public IActionResult AdminCourse() { 
            return View();
        }

        public IActionResult AdminSubCourse()
        {
            return View();
        }

        public IActionResult AdminLesson()
        {
            return View();
        }

        public IActionResult AdminAttachment()
        {
            return View();
        }

        public IActionResult AdminAssignment()
        {
            return View();
        }
        public IActionResult AdminMCQ()
        {
            return View();
        }
        public IActionResult AdminSubscription()
        {
            return View();
        }
    }
}
