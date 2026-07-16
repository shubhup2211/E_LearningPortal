using ELearningPortal.Interfaces.ISuperAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.SuperAdmin
{
    [Authorize(Roles = "Super Admin")]
    public class SubscriptionApprovalController : Controller
    {
        ISubscriptionApprovalService subAppService;
        public SubscriptionApprovalController(ISubscriptionApprovalService subAppService)
        {
            this.subAppService = subAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SuperSubscription()
        {
            return View();
        }
        public IActionResult SuperSubscriptionApprove()
        {
            return View();
        }
    }
}
