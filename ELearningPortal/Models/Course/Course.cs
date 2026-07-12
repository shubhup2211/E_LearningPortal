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
        [StringLength(150)]
        public string CourseName { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string? Image { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public ApprovalStatus ApprovalStatus { get; set; }

        [Required]
        [ForeignKey(nameof(CreatedByUser))]
        public int CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public ICollection<SubCourse>? SubCourses { get; set; }
        public ICollection<UserCourse>? UserCourses { get; set; }
        public ICollection<SubscriptionCourse>? SubscriptionCourses { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Certificate>? Certificates { get; set; }
    }
}
