using ELearning.Data;
using ELearning.Enums;
using ELearning.Models.Authentication;
using ELearningPortal.Helpers;
using ELearningPortal.Interfaces;
using ELearningPortal.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ELearningPortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext db;

        public AuthService(AppDbContext db)
        {
            this.db = db;
        }

        // Verifies email + password. Returns User (with Role & Branch) if valid & active, else null.
        public ELearning.Models.Authentication.User? ValidateUser(LoginViewModel model)
        {
            var user = db.Users
                .Include(u => u.Role)
                .Include(u => u.Branch)
                .FirstOrDefault(u => u.Email == model.Email && u.Status == Status.Active);

            if (user == null) return null;

            if (!CommonHelper.VerifyPassword(model.Password, user.PasswordHash))
                return null;

            // Update last login (small quality of life)
            user.LastLoginAt = DateTime.Now;
            db.SaveChanges();

            return user;
        }

        public ELearning.Models.Authentication.User? GetUserByEmail(string email)
        {
            return db.Users
                .Include(u => u.Role)
                .Include(u => u.Branch)
                .FirstOrDefault(u => u.Email == email);
        }
    }
}
