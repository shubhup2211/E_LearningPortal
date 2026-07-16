using ELearning.Enums;
using ELearning.Models.Courses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningPortal.Models.Course
{
    public class SubCourseView
    {
        [Key]
        public int SubCourseId { get; set; }
        [Required(ErrorMessage ="Required Field")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public string SubCourseName { get; set; } = null!;
        [Required(ErrorMessage = "Required Field")]
        public string? Description { get; set; }
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Must be integer")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Number must be 1 or greater")]
        public int DisplayOrder { get; set; }
        [Required(ErrorMessage = "Required Field")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public IFormFile? UploadedImage { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<SubCourse> SubCourseList { get; set; } = new();
    }
}
