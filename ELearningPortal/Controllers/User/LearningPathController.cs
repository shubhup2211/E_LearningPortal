using ELearningPortal.Interfaces.IUser;
using ELearningPortal.Services.User;
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



        public async Task<IActionResult> GetCertificate(int courseId)
        {
            int userId = 3;

            var certificate = await _learningPathService
                .GetCertificateAsync(userId, courseId);

            if (certificate == null)
                return Json(null);

            return Json(new
            {
                certificateNo = certificate.CertificateNo,
                generatedDate = certificate.GeneratedDate.ToString("dd MMM yyyy"),
                pdf = certificate.CertificatePath
            });
        }










    }
}