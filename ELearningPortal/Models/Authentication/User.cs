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
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Location { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Pincode { get; set; } = null!;

        public Gender? Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactPerson { get; set; } = null!;

        [StringLength(255)]
        public string? ProfileImage { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; } = null!;

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public ICollection<Course>? CreatedCourses { get; set; }
        public ICollection<AssignmentSubmission>? AssignmentSubmissions { get; set; }
        public ICollection<AssignmentSubmission>? ReviewedSubmissions { get; set; }
        public ICollection<MCQResult>? MCQResults { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<UserCourse>? UserCourses { get; set; }
        public ICollection<UserSubCourse>? UserSubCourses { get; set; }
        public ICollection<UserSubscription>? UserSubscriptions { get; set; }
        public ICollection<LessonProgress>? LessonProgresses { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Certificate>? Certificates { get; set; }
    }
}
