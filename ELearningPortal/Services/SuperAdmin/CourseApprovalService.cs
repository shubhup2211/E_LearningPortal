using ELearning.Data;
using ELearningPortal.Interfaces.ISuperAdmin;

namespace ELearningPortal.Services.SuperAdmin
{
    public class CourseApprovalService : ICourseApprovalService
    {
        private readonly AppDbContext db;
        public CourseApprovalService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
