using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    public class ProfileController : Controller
    {
        IProfileService profileService;
        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
