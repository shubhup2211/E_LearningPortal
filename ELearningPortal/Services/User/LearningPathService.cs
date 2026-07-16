using ELearning.Data;
using ELearning.Models.Courses;
using ELearning.Models.Learning;
using ELearningPortal.Interfaces.IUser;
using Microsoft.EntityFrameworkCore;

namespace ELearningPortal.Services.User
{
    public class LearningPathService : ILearningPathService
    {
        private readonly AppDbContext db;

        public LearningPathService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Course>> GetCompletedCoursesAsync(int userId)
        {
            return await db.Certificates
                .Where(x => x.UserId == userId && x.CourseId != null)
                .Include(x => x.Course)
                    .ThenInclude(c => c.Branch)
                .Select(x => x.Course!)
                .ToListAsync();
        }



        public async Task<Certificate?> GetCertificateAsync(int userId, int courseId)
        {
            return await db.Certificates
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.CourseId == courseId);
        }







    }
}