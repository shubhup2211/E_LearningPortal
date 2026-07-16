using ELearning.Models.Courses;

namespace ELearningPortal.Interfaces.IUser
{
    public interface IDashboardService
    {
        Task<List<Course>> GetDashboardCoursesAsync(int userId);

        Task<int> GetInProgressCoursesAsync(int userId);

        Task<int> GetCompletedCoursesAsync(int userId);

        Task<double> GetAverageCompletionAsync(int userId);

        Task<Dictionary<int, double>> GetCourseProgressAsync(int userId);
    }
}