using ELearningPortal.Interfaces.ISuperAdmin;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.SuperAdmin
{
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
    }
}
