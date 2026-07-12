using ELearning.Data;
using ELearningPortal.Interfaces.IAdmin;

namespace ELearningPortal.Services.Admin
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext db;
        public CourseService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
