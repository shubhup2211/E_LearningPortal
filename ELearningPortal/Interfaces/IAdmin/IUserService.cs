using ELearning.Models.Authentication;
using ELearningPortal.Models.Authentication;

namespace ELearningPortal.Interfaces.IAdmin
{
    public interface IUserService
    {
        List<User> fetchUsers();
        List<User> fetchUsersByBranch(int branchId);
        List<Role> fetchRoles();
        List<Branch> fetchBranches();

        void addUsers(UserViewModel model);
        void updateUsers(UserViewModel model);
        UserViewModel GetUserById(int id);
        void deleteUsers(int id);
    }
}
