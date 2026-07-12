using ELearning.Data;
using ELearningPortal.Interfaces.IAdmin;

namespace ELearningPortal.Services.Admin
{
    public class LessonService : ILessonService
    {
        private readonly AppDbContext db;
        public LessonService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
