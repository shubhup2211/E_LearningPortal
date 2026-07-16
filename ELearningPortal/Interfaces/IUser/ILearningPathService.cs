using ELearning.Models.Courses;
using ELearning.Models.Learning;

namespace ELearningPortal.Interfaces.IUser
{
    public interface ILearningPathService
    {
        Task<List<Course>> GetCompletedCoursesAsync(int userId);

        Task<Certificate?> GetCertificateAsync(int userId, int courseId);


    }
}