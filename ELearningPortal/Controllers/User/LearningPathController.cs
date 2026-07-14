using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    public class LearningPathController : Controller
    {
        private readonly ILearningPathService _learningPathService;

        public LearningPathController(ILearningPathService learningPathService)
        {
            _learningPathService = learningPathService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 3;

            var courses = await _learningPathService.GetCompletedCoursesAsync(userId);

            return View(courses);
        }
    }
}