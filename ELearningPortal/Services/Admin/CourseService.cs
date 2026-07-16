using ELearning.Data;
using ELearning.Enums;
using ELearning.Models.Authentication;
using ELearning.Models.Courses;
using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Models.Course;
using Microsoft.EntityFrameworkCore;

namespace ELearningPortal.Services.Admin
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;
        public CourseService(AppDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public void AddSubcourse(SubCourseView model)
        {
            string filePath = "";

            if (model.UploadedImage != null) { 

                filePath = "Content/Images/" + model.UploadedImage.FileName;
                string fullPath = Path.Combine(env.WebRootPath, filePath);
                UploadFile(model.UploadedImage, fullPath); 
            }


            SubCourse subCourse = new SubCourse()
            {
                CourseId = model.CourseId,
                SubCourseName = model.SubCourseName,
                Description = model.Description,
                Price = model.Price,
                DisplayOrder = model.DisplayOrder,
                Image = filePath,
                Status = model.Status,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            db.SubCourses.Add(subCourse);
            db.SaveChanges();
        }

        private void UploadFile(IFormFile uploadedImage, string fullPath)
        {
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                uploadedImage.CopyTo(fileStream);
           }
        }

        public List<Course> GetCourseList()
        {
            var courseList = db.Courses.Include(x=> x.Branch).Include(y=> y.CreatedByUser).ToList();
            return courseList;
        }

        public List<SubCourse> GetSubCourseList()
        {
            var subcourseList = db.SubCourses.Include(x=> x.Course).ToList();
            return subcourseList;
        }

        public SubCourseView GetSubCourse(int id)
        {
            var scourse = db.SubCourses.Find(id);

            if (scourse == null) return null;

            return new SubCourseView
            {
                SubCourseId = scourse.SubCourseId,
                CourseId = scourse.CourseId,
                SubCourseName = scourse.SubCourseName,
                Description = scourse.Description,
                Price = scourse.Price,
                DisplayOrder = scourse.DisplayOrder,
                Status = scourse.Status,
                ModifiedAt = DateTime.Now
            };
        }

        public void UpdateSubCourse(SubCourseView model)
        {
            var subCourse = db.SubCourses.Find(model.SubCourseId);

                if (subCourse != null)
                {
                    if (model.UploadedImage != null)
                    {
                        string filePath = "Content/Images/" + model.UploadedImage.FileName;
                        string fullPath = Path.Combine(env.WebRootPath, filePath);

                        UploadFile(model.UploadedImage, fullPath);

                        subCourse.Image = filePath;
                    }

                    subCourse.SubCourseId = model.SubCourseId;
                    subCourse.CourseId = model.CourseId;
                    subCourse.SubCourseName = model.SubCourseName;
                    subCourse.Description = model.Description;
                    subCourse.Price = model.Price;
                    subCourse.DisplayOrder = model.DisplayOrder;
                    subCourse.Status = model.Status;
                    subCourse.ModifiedAt = DateTime.Now;
                }

                db.SaveChanges();
            }

        public void DeleteSubcourse(int id)
        {
            var sddata = db.SubCourses.Find(id);

            if(sddata != null)
            {
                db.Remove(sddata);
                db.SaveChanges();
            }
        }

        public void AddCourse(CourseView model)
        {
            string filePath = "";

            if (model.UploadedCImage != null)
            {

                filePath = "Content/Images/" + model.UploadedCImage.FileName;
                string fullPath = Path.Combine(env.WebRootPath, filePath);
                UploadFile(model.UploadedCImage, fullPath);
            }

            Course course = new Course()
            {
                BranchId = model.BranchId,
                CourseName = model.CourseName,
                Description = model.Description,
                ApprovalStatus = model.ApprovalStatus,
                Image = filePath,
                CreatedBy = model.CreatedBy,
                Status = model.Status,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

                db.Courses.Add(course);
                int result = db.SaveChanges();

        }

        public CourseView GetCourse(int id)
        {
            var cCourse = db.Courses.Find(id);
            if (cCourse == null) return null;
            return new CourseView
            {
                CourseId = cCourse.CourseId,
                BranchId = cCourse.BranchId,
                CourseName = cCourse.CourseName,
                Description = cCourse.Description,
                CreatedBy = cCourse.CreatedBy,
                Status = cCourse.Status
            };
        }

        public void UpdateCourse(CourseView model)
        {

            var course = db.Courses.Find(model.CourseId);
            if (course != null) {
                if (model.UploadedCImage != null)
                {
                    string filePath = "Content/Images/" + model.UploadedCImage.FileName;
                    string fullPath = Path.Combine(env.WebRootPath, filePath);

                    UploadFile(model.UploadedCImage, fullPath);

                    course.Image = filePath;
                }
                course.CourseName = model.CourseName;
                course.Description = model.Description;
                course.Status = model.Status;
                course.ModifiedAt = DateTime.Now;
            }
            db.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            var dCourse = db.Courses.Find(id);

            if (dCourse != null) 
            {
                db.Courses.Remove(dCourse);
                db.SaveChanges();
            }
        }

        public List<Branch> GetBranches()
        {
            var bdata = db.Branches.ToList();
            return bdata;
        }

        public List<ELearning.Models.Authentication.User> GetUsers()
        {
            var udata = db.Users.ToList();
            return udata;
        }
    }
}
