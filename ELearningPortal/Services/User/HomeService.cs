using ELearning.Data;
using ELearningPortal.Interfaces.IUser;

namespace ELearningPortal.Services.User
{
    public class HomeService : IHomeService
    {
        private readonly AppDbContext db;
        public HomeService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
