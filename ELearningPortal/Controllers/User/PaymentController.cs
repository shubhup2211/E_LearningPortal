using ELearningPortal.Configuration;
using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ELearningPortal.Controllers.User
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly RazorpaySettings settings;

        public PaymentController(
            IPaymentService paymentService,
            IOptions<RazorpaySettings> options)
        {
            this.paymentService = paymentService;
            settings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int courseId)
        {
            int userId = 3; // Later from Login

            var order = await paymentService.CreateOrderAsync(courseId, userId);

            return Json(new
            {
                key = settings.KeyId,
                orderId = order["id"].ToString(),
                amount = order["amount"],
                currency = order["currency"]
            });
        }

        [HttpPost]
        public async Task<IActionResult> CompletePayment(
            int courseId,
            string paymentId,
            string paymentMethod)
        {
            int userId = 3;

            bool result =
                await paymentService.SavePaymentAsync(
                    userId,
                    courseId,
                    paymentId,
                    paymentMethod);

            return Json(new
            {
                success = result
            });
        }
    }
}