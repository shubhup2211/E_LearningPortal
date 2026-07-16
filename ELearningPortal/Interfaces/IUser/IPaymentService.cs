using Razorpay.Api;

namespace ELearningPortal.Interfaces.IUser
{
    public interface IPaymentService
    {
        Task<Order> CreateOrderAsync(int courseId, int userId);

        Task<bool> SavePaymentAsync(
    int userId,
    int courseId,
    string paymentId,
    string paymentMethod);
    }
}