using ELearning.Data;
using ELearningPortal.Interfaces.IAdmin;

namespace ELearningPortal.Services.Admin
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly AppDbContext db;
        public SubscriptionService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
