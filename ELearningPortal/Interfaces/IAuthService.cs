using ELearning.Models.Authentication;
using ELearningPortal.Models.Authentication;

namespace ELearningPortal.Interfaces
{
    // Auth service – handles login. Returns the User (with Role & Branch loaded) or null if failed.
    public interface IAuthService
    {
        User? ValidateUser(LoginViewModel model);
        User? GetUserByEmail(string email);
    }
}
