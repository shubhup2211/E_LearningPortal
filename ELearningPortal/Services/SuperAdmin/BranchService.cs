using ELearning.Data;
using ELearningPortal.Interfaces.ISuperAdmin;

namespace ELearningPortal.Services.SuperAdmin
{
    public class BranchService : IBranchService
    {
        private readonly AppDbContext db;
        public BranchService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
