using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Courses;
using ELearning.Models.Purchases;
using ELearning.Models.Learning;

namespace ELearning.Models.Authentication
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        public Gender? Gender { get; set; }

        [Required]
        public string? ProfileImage { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        [Required]
        public DateTime LastLoginAt { get; set; }

        // Navigation properties
        public List<Course> CreatedCourses { get; set; } = new();
        public List<AssignmentSubmission> AssignmentSubmissions { get; set; } = new();
        public List<AssignmentSubmission> ReviewedSubmissions { get; set; } = new();
        public List<MCQResult> MCQResults { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
        public List<UserCourse> UserCourses { get; set; } = new();
        public List<UserSubCourse> UserSubCourses { get; set; } = new();
        public List<UserSubscription> UserSubscriptions { get; set; } = new();
        public List<LessonProgress> LessonProgresses { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
        public List<Certificate> Certificates { get; set; } = new();
    }
}
