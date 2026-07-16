using ELearning.Enums;
using ELearning.Models.Authentication;
using ELearning.Models.Learning;
using ELearning.Models.Purchases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Models.Courses
{
    public class SubCourse
    {
        [Key]
        public int SubCourseId { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public string SubCourseName { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public string? Image { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public List<Lesson> Lessons { get; set; } = new();
        public List<UserSubCourse> UserSubCourses { get; set; } = new();
        public List<Certificate> Certificates { get; set; } = new();
    }
}
