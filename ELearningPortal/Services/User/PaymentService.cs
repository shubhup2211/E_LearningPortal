using ELearning.Data;
using ELearningPortal.Interfaces.IUser;

namespace ELearningPortal.Services.User
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext db;
        public PaymentService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
