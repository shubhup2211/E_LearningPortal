using Microsoft.EntityFrameworkCore;
using ELearning.Models.Authentication;
using ELearning.Models.Courses;
using ELearning.Models.Purchases;
using ELearning.Models.Learning;

namespace ELearning.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Authentication table
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }

        // Course Tables
        public DbSet<Course> Courses { get; set; }
        public DbSet<SubCourse> SubCourses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

        // Purchase Tables
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserSubCourse> UserSubCourses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionCourse> SubscriptionCourses { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        // Learning Tables
        public DbSet<LessonProgress> LessonProgresses { get; set; }
        public DbSet<MCQResult> MCQResults { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Branch>()
                .HasIndex(b => b.Email)
                .IsUnique();

            // Authentication Relationships

            // Role -> User (Restrict on delete of Role)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Branch -> Users (Restrict)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Branch)
                .WithMany(b => b.Users)
                .HasForeignKey(u => u.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course Relationships

            // User -> Course (CreatedBy)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CreatedCourses)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> SubCourse (Cascade)
            modelBuilder.Entity<SubCourse>()
                .HasOne(s => s.Course)
                .WithMany(c => c.SubCourses)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Branch -> SubCourse (Restrict - per Branch -> Course restrict rule)
            modelBuilder.Entity<SubCourse>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.SubCourses)
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // SubCourse -> Lesson (Cascade)
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.SubCourse)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.SubCourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lesson -> Attachment (Cascade)
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Lesson)
                .WithMany(l => l.Attachments)
                .HasForeignKey(a => a.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lesson -> Assignment (Cascade)
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Lesson)
                .WithMany(l => l.Assignments)
                .HasForeignKey(a => a.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lesson -> Question (Cascade)
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.Questions)
                .HasForeignKey(q => q.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question -> QuestionOption (Cascade)
            modelBuilder.Entity<QuestionOption>()
                .HasOne(o => o.Question)
                .WithMany(q => q.QuestionOptions)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Assignment -> AssignmentSubmission (Cascade)
            modelBuilder.Entity<AssignmentSubmission>()
                .HasOne(s => s.Assignment)
                .WithMany(a => a.AssignmentSubmissions)
                .HasForeignKey(s => s.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> AssignmentSubmission (submitter) - Restrict to avoid multi-cascade path
            modelBuilder.Entity<AssignmentSubmission>()
                .HasOne(s => s.User)
                .WithMany(u => u.AssignmentSubmissions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User -> AssignmentSubmission (reviewer) - Restrict
            modelBuilder.Entity<AssignmentSubmission>()
                .HasOne(s => s.ReviewedByUser)
                .WithMany(u => u.ReviewedSubmissions)
                .HasForeignKey(s => s.ReviewedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Purchase Relationships

            // Users -> Payment (Restrict)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserCourse: User (Restrict), Course (Restrict), Payment (Restrict)
            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCourses)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(uc => uc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.Payment)
                .WithMany(p => p.UserCourses)
                .HasForeignKey(uc => uc.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserSubCourse: User (Restrict), SubCourse (Restrict), Payment (Restrict)
            modelBuilder.Entity<UserSubCourse>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSubCourses)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubCourse>()
                .HasOne(us => us.SubCourse)
                .WithMany(s => s.UserSubCourses)
                .HasForeignKey(us => us.SubCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubCourse>()
                .HasOne(us => us.Payment)
                .WithMany(p => p.UserSubCourses)
                .HasForeignKey(us => us.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            // SubscriptionCourse: Subscription (Cascade), Course (Restrict)
            modelBuilder.Entity<SubscriptionCourse>()
                .HasOne(sc => sc.Subscription)
                .WithMany(s => s.SubscriptionCourses)
                .HasForeignKey(sc => sc.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubscriptionCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.SubscriptionCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserSubscription: User (Restrict), Subscription (Restrict), Payment (Restrict)
            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSubscriptions)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Subscription)
                .WithMany(s => s.UserSubscriptions)
                .HasForeignKey(us => us.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Payment)
                .WithMany(p => p.UserSubscriptions)
                .HasForeignKey(us => us.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Learning Relationships

            // LessonProgress: User (Restrict), Lesson (Cascade)
            modelBuilder.Entity<LessonProgress>()
                .HasOne(lp => lp.User)
                .WithMany(u => u.LessonProgresses)
                .HasForeignKey(lp => lp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LessonProgress>()
                .HasOne(lp => lp.Lesson)
                .WithMany(l => l.LessonProgresses)
                .HasForeignKey(lp => lp.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // MCQResult: User (Restrict), Lesson (Cascade)
            modelBuilder.Entity<MCQResult>()
                .HasOne(m => m.User)
                .WithMany(u => u.MCQResults)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MCQResult>()
                .HasOne(m => m.Lesson)
                .WithMany(l => l.MCQResults)
                .HasForeignKey(m => m.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Users -> Rating (Restrict)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Rating (Restrict to avoid conflict with User path)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Users -> Certificate (Restrict)
            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.User)
                .WithMany(u => u.Certificates)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Certificate (Restrict)
            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Course)
                .WithMany(cs => cs.Certificates)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
