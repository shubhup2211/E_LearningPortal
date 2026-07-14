using ELearningPortal.Interfaces.ISuperAdmin;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.SuperAdmin
{
    public class CourseApprovalController : Controller
    {
        ICourseApprovalService couApplService;
        public CourseApprovalController(ICourseApprovalService couApplService)
        {
            this.couApplService = couApplService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SuperCourse()
        {
            return View();
        }
        public IActionResult SuperCourseApprove()
        {
            return View();
        }
    }
}
