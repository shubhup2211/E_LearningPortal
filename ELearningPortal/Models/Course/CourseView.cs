using ELearning.Enums;
using ELearning.Models.Authentication;
using ELearningPortal.Models.Course;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningPortal.Models.Course
{
    public class CourseView
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public string CourseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required Field")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public IFormFile? UploadedCImage { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public ApprovalStatus ApprovalStatus { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public List<ELearning.Models.Courses.Course>? coursesList { get; set; } = new();
        
    }
}
