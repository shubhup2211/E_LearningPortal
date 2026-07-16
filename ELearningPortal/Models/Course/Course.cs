using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Authentication;
using ELearning.Models.Purchases;
using ELearning.Models.Learning;

namespace ELearning.Models.Courses
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public string? Image { get; set; }

        [Required]
        public ApprovalStatus ApprovalStatus { get; set; }

        [Required]
        [ForeignKey(nameof(CreatedByUser))]
        public int CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation Properties
        public List<SubCourse> SubCourses { get; set; } = new();
        public List<UserCourse> UserCourses { get; set; } = new();
        public List<SubscriptionCourse> SubscriptionCourses { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
        public List<Certificate> Certificates { get; set; } = new();
    }
}
