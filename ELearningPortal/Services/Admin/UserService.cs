using ELearning.Data;
using ELearningPortal.Interfaces.IAdmin;
using ELearning.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearningPortal.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly AppDbContext db;
        public UserService(AppDbContext db)
        {
            this.db = db;
        }

        public List<ELearning.Models.Authentication.User> fetchUsers()
        {
            var udata = db.Users.Include(x=> x.Role).ToList();
            return udata;
        }
    }
}
