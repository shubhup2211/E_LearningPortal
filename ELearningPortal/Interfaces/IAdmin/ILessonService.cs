using ELearning.Models.Courses;
using ELearningPortal.Models.Course;

namespace ELearningPortal.Interfaces.IAdmin
{
    public interface ILessonService
    {
        List<Lesson> getLessons();
        IEnumerable<SubCourse> GetSubCoursesByBranch(int branchId);
        void AddLessons(LessonView lesson);
        LessonView getLessons(int id);
        void UpdateLessons(LessonView lesson);
        void DeleteLessons(int id);

    }
}
