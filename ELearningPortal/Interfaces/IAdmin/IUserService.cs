using ELearning.Models.Authentication;

namespace ELearningPortal.Interfaces.IAdmin
{
    public interface IUserService
    {
        List<User> fetchUsers();
    }
}
