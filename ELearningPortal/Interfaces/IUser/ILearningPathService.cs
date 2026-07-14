using ELearning.Models.Courses;

namespace ELearningPortal.Interfaces.IUser
{
    public interface ILearningPathService
    {
        Task<List<Course>> GetCompletedCoursesAsync(int userId);
    }
}