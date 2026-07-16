using ELearning.Models.Authentication;
using ELearningPortal.Helpers;
using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Models.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearningPortal.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult AddLessons()
        {

            int BranchId = User.GetBranchId();

            LessonView model = new LessonView();
            model.Lessonlist = lessonService.getLessons();
            var subCourses = lessonService.GetSubCoursesByBranch(BranchId);

            ViewBag.SubCourse = new SelectList(subCourses, "SubCourseId", "SubCourseName");
            return View(model);
        }

        [HttpPost]
        public IActionResult AddLessons(LessonView lessons)
        {
            int BranchId = User.GetBranchId();

            if (!ModelState.IsValid)
            {
                lessons.Lessonlist = lessonService.getLessons();
                var subCourses = lessonService.GetSubCoursesByBranch(BranchId);

                ViewBag.SubCourse = new SelectList(subCourses, "SubCourseId", "SubCourseName");
                return View(lessons);

            }

            lessonService.AddLessons(lessons);
            TempData["successmsg"] = "Lesson added successfully";
            return RedirectToAction("AddLessons");

        }

        public IActionResult EditLesson(int id) 
        {
            int BranchId = User.GetBranchId();

            var editLesson = lessonService.getLessons(id);
            var subCourses = lessonService.GetSubCoursesByBranch(BranchId);
            ViewBag.SubCourse = new SelectList(subCourses, "SubCourseId", "SubCourseName");
            return View(editLesson);
        }

        [HttpPost]
        public IActionResult UpdateLesson(LessonView lesson)
        {
            lessonService.UpdateLessons(lesson);
            @TempData["warningmsg"] = "Lesson Updated successfully";
            return RedirectToAction("AddLessons");
        }

        public IActionResult DeleteLesson(int id)
        {
            lessonService.DeleteLessons(id);
            @TempData["warningmsg"] = "Lesson Updated successfully";
            return RedirectToAction("AddLessons");
        }
    }
}
