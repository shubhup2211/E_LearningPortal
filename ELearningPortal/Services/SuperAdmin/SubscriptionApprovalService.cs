using ELearning.Data;
using ELearningPortal.Interfaces.ISuperAdmin;

namespace ELearningPortal.Services.SuperAdmin
{
    public class SubscriptionApprovalService : ISubscriptionApprovalService
    {
        private readonly AppDbContext db;
        public SubscriptionApprovalService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
