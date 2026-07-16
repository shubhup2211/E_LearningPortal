using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    [Authorize(Roles = "Student")]
    public class PaymentController : Controller
    {
        IPaymentService payService;
        public PaymentController(IPaymentService payService)
        {
            this.payService = payService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
