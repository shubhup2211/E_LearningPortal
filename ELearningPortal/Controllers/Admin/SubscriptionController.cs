using ELearningPortal.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.Admin
{
    public class SubscriptionController : Controller
    {
        ISubscriptionService subsService;
        public SubscriptionController(ISubscriptionService subsService)
        {
            this.subsService = subsService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
