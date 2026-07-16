using ELearning.Data;
using ELearningPortal.Interfaces.IUser;

namespace ELearningPortal.Services.User
{
    public class ProfileService : IProfileService
    {
        private readonly AppDbContext db;
        public ProfileService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
