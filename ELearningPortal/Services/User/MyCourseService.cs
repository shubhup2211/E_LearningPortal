using ELearning.Data;
using ELearningPortal.Interfaces.IUser;

namespace ELearningPortal.Services.User
{
    public class MyCourseService : IMyCourseService
    {
        private readonly AppDbContext db;
        public MyCourseService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
