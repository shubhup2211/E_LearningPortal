using ELearning.Data;
using ELearning.Enums;
using ELearning.Models.Courses;
using ELearningPortal.Interfaces.IUser;
using Microsoft.EntityFrameworkCore;

namespace ELearningPortal.Services.User
{
    public class MyCourseService : IMyCourseService
    {
        private readonly AppDbContext db;
        public MyCourseService(AppDbContext db)
        {
            this.db = db;
        }





        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await db.Courses
                .Include(x => x.Branch)
                .Include(x => x.SubCourses)
                    .ThenInclude(x => x.Lessons)
                .Include(x => x.Ratings)
                .Where(x => x.Status == Status.Active)
                .ToListAsync();
        }







    }
}
