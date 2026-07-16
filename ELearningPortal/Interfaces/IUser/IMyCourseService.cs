using ELearningPortal.Models.ViewModels;

namespace ELearningPortal.Interfaces.IUser
{
    public interface IMyCourseService
    {

        // Right side "Course Content" tree + left side current lesson load karne ke liye
        Task<MyCourseIndexViewModel> GetCourseContentAsync(int userId, int subCourseId, int? selectedLessonId);

        // Jab user sidebar se koi lesson click kare (AJAX)
        Task<LessonDetailViewModel?> GetLessonDetailAsync(int userId, int lessonId);

        // YouTube video ka "onStateChange -> ENDED" event aane par call hoga
        Task<bool> MarkVideoCompletedAsync(int userId, int lessonId);

        // Video complete hone ke baad 5 MCQ fetch karne ke liye
        Task<List<McqQuestionViewModel>> GetMcqQuestionsAsync(int lessonId);

        // MCQ submit (AJAX POST)
        Task<McqResultViewModel> SubmitMcqAsync(int userId, McqSubmitViewModel model);

        // Assignment file upload (AJAX POST, multipart/form-data)
        Task<bool> SubmitAssignmentAsync(int userId, int lessonId, IFormFile file);
    }
}
