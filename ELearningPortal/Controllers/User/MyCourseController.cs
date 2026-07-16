using System.Security.Claims;
using ELearningPortal.Interfaces.IUser;
using ELearningPortal.Models.ViewModels;
//using ELearningPortal.Models.ViewModels.MyCourse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.User
{
    //[Authorize] // Sirf logged-in users hi is page ko access kar sakein
    public class MyCourseController : Controller
    {
        private readonly IMyCourseService myCourseService;
        
        public MyCourseController(IMyCourseService myCourseService)
        {
            this.myCourseService = myCourseService;
        }


        [HttpGet]
        public async Task<IActionResult> SwitchSubCourse(int subCourseId)
        {
            int userId = GetLoggedInUserId();

            var vm = await myCourseService.GetCourseContentAsync(userId, subCourseId, null);
            return PartialView("_CourseContentPartial", vm);
        }

        // ================= MAIN PAGE (Full Load) =================
        // URL: /MyCourse/Index?subCourseId=5
        [HttpGet]
        public async Task<IActionResult> Index(int subCourseId, int? lessonId)
        {
            int userId = GetLoggedInUserId();

            var vm = await myCourseService.GetCourseContentAsync(userId, subCourseId, lessonId);
            return View(vm);
        }

        // ================= SIDEBAR LESSON CLICK (AJAX - no page reload) =================
        // URL: /MyCourse/GetLesson?lessonId=12
        [HttpGet]
        public async Task<IActionResult> GetLesson(int lessonId)
        {
            int userId = GetLoggedInUserId();

            var lesson = await myCourseService.GetLessonDetailAsync(userId, lessonId);

            if (lesson == null)
                return NotFound(new { message = "Lesson not found." });

            if (!lesson.IsUnlocked)
                return StatusCode(403, new { message = "This Lessson is locked complete previous Lesson first....." });
            
            // Partial view return karenge jo left side video player area replace karega
            return PartialView("_LessonPlayerPartial", lesson);
        }

        // ================= YOUTUBE VIDEO END EVENT (AJAX) =================
        // Called when YT.Player onStateChange -> YT.PlayerState.ENDED
        [HttpPost]
        public async Task<IActionResult> MarkVideoCompleted([FromBody] MarkVideoRequest request)
        {
            int userId = GetLoggedInUserId();
            var result = await myCourseService.MarkVideoCompletedAsync(userId, request.LessonId);

            return Json(new { success = result });
        }

        // ================= GET MCQ QUESTIONS (AJAX - Modal Open) =================
        // URL: /MyCourse/GetMcqQuestions?lessonId=12
        [HttpGet]
        public async Task<IActionResult> GetMcqQuestions(int lessonId)
        {
            var questions = await myCourseService.GetMcqQuestionsAsync(lessonId);

            if (questions == null || questions.Count == 0)
                return Json(new { success = false, message = "Is Lesson Ke Liye MCQ Available Nahi Hai." });

            return PartialView("_McqModalPartial", questions);
        }

        // ================= SUBMIT MCQ (AJAX POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitMcq([FromBody] McqSubmitViewModel model)
        {
            if (model.Answers == null || model.Answers.Count != 5)
                return BadRequest(new { message = "Answer All 5 MCQ Question." });

            int userId = GetLoggedInUserId();
            var result = await myCourseService.SubmitMcqAsync(userId, model);

            return Json(result);
        }

        // ================= SUBMIT ASSIGNMENT (AJAX POST - multipart/form-data) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitAssignment(int lessonId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Please Select A File." });

            // Basic file validation
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".zip", ".rar" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext))
                return BadRequest(new { message = "Invalid File Type. Allowed: PDF, DOC, DOCX, ZIP, RAR." });

            if (file.Length > 10 * 1024 * 1024) // 10 MB limit
                return BadRequest(new { message = "File Size Should Fall Under 10 MB ." });

            int userId = GetLoggedInUserId();
            bool success = await myCourseService.SubmitAssignmentAsync(userId, lessonId, file);

            if (!success)
                return BadRequest(new { message = "Assignment Not Able To Submit. First Pass MCQ ." });

            return Json(new { success = true, message = "Assignment Submitted Successfully! Next Lesson Unlocked." });
        }

        // ================= HELPER =================
        private int GetLoggedInUserId()
        {
            //var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //return int.Parse(claim!);

            return 1;
        }
    }

    // Small request DTO for MarkVideoCompleted endpoint
    public class MarkVideoRequest
    {
        public int LessonId { get; set; }
    }



}