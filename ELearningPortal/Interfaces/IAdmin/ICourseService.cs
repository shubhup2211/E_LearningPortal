using ELearning.Models.Authentication;
using ELearning.Models.Courses;
using ELearningPortal.Models.Course;

namespace ELearningPortal.Interfaces.IAdmin
{
    public interface ICourseService
    {
        List<SubCourse> GetSubCourseList();
        List<Course> GetCourseList();
        List<Branch> GetBranches();
        List<User> GetUsers();

        void AddSubcourse(SubCourseView model);
        SubCourseView GetSubCourse(int id);
        void UpdateSubCourse(SubCourseView model);
        void DeleteSubcourse(int id);


        void AddCourse(CourseView model);
        CourseView GetCourse(int id);
        void UpdateCourse(CourseView model);
        void DeleteCourse(int id);

    }
}
