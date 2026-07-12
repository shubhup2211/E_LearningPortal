using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
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
