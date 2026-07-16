using ELearning.Data;
using ELearning.Models.Courses;
using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Models.Course;
using Microsoft.EntityFrameworkCore;

namespace ELearningPortal.Services.Admin
{
    public class LessonService : ILessonService
    {
        private readonly AppDbContext db;
        public LessonService(AppDbContext db)
        {
            this.db = db;
        }

        public List<Lesson> getLessons()
        {
            var ldata = db.Lessons.ToList();
            return ldata;
        }

        public void AddLessons(LessonView lesson)
        {
            var addles = new Lesson()
            {
                SubCourseId = lesson.SubCourseId,
                LessonTitle = lesson.LessonTitle,
                VideoUrl = lesson.VideoUrl,
                Duration = lesson.Duration,
                DisplayOrder = lesson.DisplayOrder,
                Status = lesson.Status,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            db.Lessons.Add(addles);
            db.SaveChanges();
        }

        public LessonView getLessons(int id)
        {
            var getId = db.Lessons.Find(id);

            if (getId == null) { return null; }

            return new LessonView
            {
                SubCourseId = getId.SubCourseId,
                LessonTitle = getId.LessonTitle,
                VideoUrl = getId.VideoUrl,
                Duration = getId.Duration,
                DisplayOrder = getId.DisplayOrder,
                Status = getId.Status
            };
                
        }

        public void UpdateLessons(LessonView lesson)
        {
            var uplesson = db.Lessons.Find(lesson.LessonId);

            if (uplesson != null)
            {
                uplesson.SubCourseId = lesson.SubCourseId;
                uplesson.LessonTitle = lesson.LessonTitle;
                uplesson.VideoUrl = lesson.VideoUrl;
                uplesson.Duration = lesson.Duration;
                uplesson.DisplayOrder = lesson.DisplayOrder;
                uplesson.Status = lesson.Status;
                uplesson.ModifiedAt = DateTime.Now;

                db.SaveChanges();
            }


        }

        public void DeleteLessons(int id)
        {

            var dellesson = db.Lessons.Find(id);
            if (dellesson != null)
            {
                db.Lessons.Remove(dellesson);
                db.SaveChanges();
            }
        }


        public IEnumerable<SubCourse> GetSubCoursesByBranch(int branchId)
        {
            var sub =  db.SubCourses.Include(sc => sc.Course) 
                      .Where(sc => sc.Course.BranchId == branchId).ToList();

            return sub;
        }

    }
}
