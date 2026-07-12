using ELearningPortal.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.Admin
{
    public class LessonController : Controller
    {
        ILessonService lessonService;
        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
