using ELearning.Data;
using ELearning.Models.Courses;
using ELearningPortal.Enums;
using ELearningPortal.Interfaces.IUser;
using Microsoft.EntityFrameworkCore;

namespace ELearningPortal.Services.User
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext db;

        public DashboardService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Course>> GetDashboardCoursesAsync(int userId)
        {
            return await db.UserCourses
                .Where(x => x.UserId == userId)
                .Include(x => x.Course)
                    .ThenInclude(c => c.Branch)
                .Include(x => x.Course)
                    .ThenInclude(c => c.SubCourses)
                        .ThenInclude(sc => sc.Lessons)
                .Select(x => x.Course)
                .ToListAsync();
        }



        public async Task<int> GetInProgressCoursesAsync(int userId)
        {
            return await db.UserCourses
                .CountAsync(x =>
                    x.UserId == userId &&
                    !db.Certificates.Any(c =>
                        c.UserId == userId &&
                        c.CourseId == x.CourseId));
        }



        public async Task<int> GetCompletedCoursesAsync(int userId)
        {
            return await db.Certificates
                .CountAsync(x =>
                    x.UserId == userId &&
                    x.CourseId != null);
        }



        public async Task<double> GetAverageCompletionAsync(int userId)
        {
            int totalLessons = await db.Lessons.CountAsync();

            int completedLessons = await db.LessonProgresses
                .CountAsync(x =>
                    x.UserId == userId &&
                    x.Status == LessonStatus.Completed);

            if (totalLessons == 0)
                return 0;

            return Math.Round((double)completedLessons / totalLessons * 100, 0);
        }


        public async Task<Dictionary<int, double>> GetCourseProgressAsync(int userId)
        {
            Dictionary<int, double> progress = new();

            var courses = await db.Courses
                .Include(c => c.SubCourses)
                    .ThenInclude(sc => sc.Lessons)
                .Where(c => c.UserCourses.Any(uc => uc.UserId == userId))
                .ToListAsync();

            foreach (var course in courses)
            {
                var lessonIds = course.SubCourses
                    .SelectMany(sc => sc.Lessons)
                    .Select(l => l.LessonId)
                    .ToList();

                int totalLessons = lessonIds.Count;

                int completedLessons = await db.LessonProgresses
                    .CountAsync(lp =>
                        lp.UserId == userId &&
                        lp.Status == LessonStatus.Completed &&
                        lessonIds.Contains(lp.LessonId));

                double percent = 0;

                if (totalLessons > 0)
                {
                    percent = Math.Round((double)completedLessons / totalLessons * 100, 0);
                }

                progress.Add(course.CourseId, percent);
            }

            return progress;
        }

    }
}