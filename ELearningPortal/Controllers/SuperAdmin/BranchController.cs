using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Interfaces.ISuperAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.SuperAdmin
{
    [Authorize(Roles = "Super Admin")]
    public class BranchController : Controller
    {
        IBranchService branchService;
        public BranchController(IBranchService branchService)
        {
            this.branchService = branchService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SuperBranch()
        {
            return View();
        }
    }
}
