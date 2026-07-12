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


    }
}
