using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Authentication;
using ELearning.Models.Purchases;

namespace ELearning.Models.Courses
{
    public class SubCourse
    {
        [Key]
        public int SubCourseId { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        [ForeignKey(nameof(Branch))]
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        [Required]
        [StringLength(150)]
        public string SubCourseName { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string? Image { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<UserSubCourse>? UserSubCourses { get; set; }
    }
}
