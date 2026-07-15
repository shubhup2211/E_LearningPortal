using ELearningPortal.Interfaces.IUser;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 3; // Later replace with logged-in user

            var courses = await dashboardService.GetDashboardCoursesAsync(userId);

            ViewBag.InProgress = await dashboardService.GetInProgressCoursesAsync(userId);

            ViewBag.Completed = await dashboardService.GetCompletedCoursesAsync(userId);

            ViewBag.Average = await dashboardService.GetAverageCompletionAsync(userId);

            ViewBag.Progress = await dashboardService.GetCourseProgressAsync(userId);

            return View(courses);

            //return RedirectToAction("Lesson");
        }
    }
}