using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Models.Authentication;
using ELearning.Models.Courses;
using ELearningPortal.Enums;

namespace ELearning.Models.Learning
{
    public class Certificate
    {
        [Key]
        public int CertificateId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        [Required]
        [ForeignKey(nameof(SubCourse))]
        public int SubCourseId { get; set; }
        public SubCourse? SubCourse { get; set; }

        [Required]
        public CertificateType CertificateType { get; set; }

        [Required]
        [StringLength(100)]
        public string CertificateNo { get; set; } = null!;

        [Required]
        public DateTime GeneratedDate { get; set; }

        [StringLength(255)]
        public string? CertificatePath { get; set; }
    }
}
