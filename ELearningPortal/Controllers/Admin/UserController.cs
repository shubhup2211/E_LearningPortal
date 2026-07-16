using ELearningPortal.Helpers;
using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearningPortal.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {
            var branchId = User.GetBranchId();        
            var udata = userService.fetchUsersByBranch(branchId);
            return View(udata);
        }

        public IActionResult AdminUser()
        {
            var branchId = User.GetBranchId();
            var udata = userService.fetchUsersByBranch(branchId);

            var rdata = userService.fetchRoles();
            var bdata = userService.fetchBranches();
            ViewBag.Role = new SelectList(rdata, "RoleId", "RoleName");
            ViewBag.Branch = new SelectList(bdata, "BranchId", "BranchName", branchId);
            ViewBag.CurrentBranchId = branchId;
            return View(udata);
        }

        [HttpPost]
        public IActionResult AdminAddUser(UserViewModel model)
        {
            model.BranchId = User.GetBranchId();
            userService.addUsers(model);
            TempData["successmsg"] = "User created & credentials emailed!";
            return RedirectToAction("AdminUser");
        }

        public IActionResult EditUser(int id)
        {
            var user = userService.GetUserById(id);
            var rdata = userService.fetchRoles();
            var bdata = userService.fetchBranches();
            ViewBag.Role = new SelectList(rdata, "RoleId", "RoleName");
            ViewBag.Branch = new SelectList(bdata, "BranchId", "BranchName");

            return View(user);
        }

        [HttpPost]
        public IActionResult AdminUpdateUser(UserViewModel model)
        {
            userService.updateUsers(model);
            TempData["warningmsg"] = "Emp Updated Successfully!!";            
            return RedirectToAction("AdminUser");
        }

        public IActionResult AdminDelete(int id)
        {
            userService.deleteUsers(id);
            TempData["dangermsg"] = "Emp Deleted Successfully!!";
            return RedirectToAction("AdminUser");
        }
        public IActionResult AdminCourse() { 
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
