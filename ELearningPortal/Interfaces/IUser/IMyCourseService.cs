using ELearning.Models.Courses;

namespace ELearningPortal.Interfaces.IUser
{
    public interface IMyCourseService
    {

        Task<List<Course>> GetAllCoursesAsync();
    }
}
