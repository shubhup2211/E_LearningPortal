using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    [Authorize(Roles = "Student")]
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
