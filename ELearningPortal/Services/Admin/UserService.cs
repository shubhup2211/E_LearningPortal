using ELearning.Data;
using ELearning.Models.Authentication;
using ELearningPortal.Helpers;
using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Models.Authentication;
using Microsoft.EntityFrameworkCore;


namespace ELearningPortal.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;
        private readonly EmailHelper emailHelper;

        public UserService(AppDbContext db, IWebHostEnvironment env, EmailHelper emailHelper)
        {
            this.db = db;
            this.env = env;
            this.emailHelper = emailHelper;
        }


        public void addUsers(UserViewModel model)
        {
            string filePath = "";
            if (model.ProfileImage != null)
            {
                filePath = "Content/Images/" + model.ProfileImage.FileName;
                string fullPath = Path.Combine(env.WebRootPath, filePath);
                UploadFile(model.ProfileImage, fullPath);
            }

            string plainPassword = model.Password;  //to send user so he can get actual pass

            var record = new ELearning.Models.Authentication.User
            {
                ProfileImage = filePath,
                RoleId = model.RoleId,
                BranchId = model.BranchId,
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = CommonHelper.HashPassword(plainPassword),  //has pass
                Phone = model.Phone,
                Gender = model.Gender,
                Status = model.Status,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                LastLoginAt = DateTime.Now
            };
            db.Users.Add(record);
            db.SaveChanges();

            var role = db.Roles.Find(model.RoleId);
            var branch = db.Branches.Find(model.BranchId);
            emailHelper.SendCredentials(
                model.Email,
                model.FullName,
                plainPassword,
                role?.RoleName ?? "User",
                branch?.BranchName ?? ""
            );
        }

        private void UploadFile(IFormFile ProfileImage, string fullPath)
        {
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                ProfileImage.CopyTo(fileStream);
            }
        }
        public void deleteUsers(int id)
        {
            var eudata = db.Users.Find(id);
            if (eudata != null)
            {
                db.Users.Remove(eudata);
                db.SaveChanges();
            }
        }

        public List<Branch> fetchBranches()
        {
            var bdata = db.Branches.ToList();
            return bdata;
        }

        public List<Role> fetchRoles()
        {
            var rdata = db.Roles.ToList();
            return rdata;
        }

        public List<ELearning.Models.Authentication.User> fetchUsers()
        {
            var udata = db.Users.Include(x => x.Role).Include(y => y.Branch).ToList();
            return udata;
        }
      
        public List<ELearning.Models.Authentication.User> fetchUsersByBranch(int branchId)
        {
            return db.Users.Include(x => x.Role).Include(y => y.Branch)
                           .Where(u => u.BranchId == branchId).ToList();
        }

        public UserViewModel GetUserById(int id)
        {
            var user = db.Users.Find(id);

            if (user == null)
                return null!;

            return new UserViewModel
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                BranchId = user.BranchId,
                Gender = user.Gender,
                Status = user.Status,
                ModifiedAt = DateTime.Now

            };
        }

        public void updateUsers(UserViewModel model)
        {
            var user = db.Users.Find(model.UserId);

            if (user != null)
            {
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.RoleId = model.RoleId;
                user.BranchId = model.BranchId;
                user.Gender = model.Gender;
                user.Status = model.Status;
                user.ModifiedAt = DateTime.Now;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    user.PasswordHash = CommonHelper.HashPassword(model.Password);
                }
            }

            if (model.ProfileImage != null)
            {
                
            }

            db.SaveChanges();
        }

    }
}
