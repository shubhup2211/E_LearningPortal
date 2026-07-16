using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Models.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearningPortal.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        ICourseService courseService;
        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminSubCourse()
        {
            SubCourseView model = new SubCourseView();
            model.SubCourseList = courseService.GetSubCourseList();
            var cdata = courseService.GetCourseList();

            ViewBag.Course = new SelectList(cdata, "CourseId", "CourseName");
            return View(model);
        }

        [HttpPost]
        public IActionResult AdminSubCourse(SubCourseView model)
        {
            if (!ModelState.IsValid)
            {
                model.SubCourseList = courseService.GetSubCourseList();

                ViewBag.Course = new SelectList(courseService.GetCourseList(),"CourseId", "CourseName");

                return View(model);
            }

            courseService.AddSubcourse(model);
            TempData["successmsg"] = "Subcourse added successfully";
            return RedirectToAction("AdminSubCourse");
        }

        public IActionResult EditSubCourse(int id)
        {
            var editSub = courseService.GetSubCourse(id);
            var cdata = courseService.GetCourseList();
            ViewBag.Course = new SelectList(cdata, "CourseId", "CourseName");
            return View(editSub);
        }

        [HttpPost]
        public IActionResult AdminUpdateSubc(SubCourseView model)
        {
            courseService.UpdateSubCourse(model);
            @TempData["warningmsg"] = "Subcourse Updated successfully";
            return RedirectToAction("AdminSubCourse");
        }

        public IActionResult DeleteSubcourse(int id)
        {
            courseService.DeleteSubcourse(id);
            @TempData["dangermsg"] = "Subcourse Deleted successfully";
            return RedirectToAction("AdminSubCourse");
        }


        public IActionResult AdminCourse()
        {
            CourseView model = new CourseView();
            model.coursesList = courseService.GetCourseList();
            var bdata = courseService.GetBranches();
            var udata = courseService.GetUsers();

            ViewBag.Branch = new SelectList(bdata, "BranchId", "BranchName");
            ViewBag.UserData = new SelectList(udata, "UserId", "FullName");
            return View(model);
        }

        [HttpPost]
        public IActionResult AdminCourse(CourseView model)
        {

            if (!ModelState.IsValid)
            {
                model.coursesList = courseService.GetCourseList();

                var bdata = courseService.GetBranches();
                var udata = courseService.GetUsers();

                ViewBag.Branch = new SelectList(bdata, "BranchId", "BranchName");
                ViewBag.UserData = new SelectList(udata, "UserId", "FullName");

                return View(model);
            }

            courseService.AddCourse(model);
            @TempData["successmsg"] = "Course added successfully";
            return RedirectToAction("AdminCourse");
        }

        public IActionResult EditCourse(int id)
        {
            var editCourse = courseService.GetCourse(id);
            var bdata = courseService.GetBranches();
            var udata = courseService.GetUsers();

            ViewBag.Branch = new SelectList(bdata, "BranchId", "BranchName");
            ViewBag.UserData = new SelectList(udata, "UserId", "FullName");
            return View(editCourse);
        }

        [HttpPost]
        public IActionResult AdminUpdateCourse(CourseView model)
        {
            courseService.UpdateCourse(model);
            @TempData["warningmsg"] = "Course Updated successfully";
            return RedirectToAction("AdminCourse");
        }

        public IActionResult DeleteCourse(int id)
        {
            courseService.DeleteCourse(id);
            @TempData["dangermsg"] = "Course Deleted successfully";
            return RedirectToAction("AdminCourse");
        }

    }
}
