using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Authentication;

namespace ELearning.Models.Courses
{
    public class AssignmentSubmission
    {
        [Key]
        public int SubmissionId { get; set; }

        [Required]
        [ForeignKey(nameof(Assignment))]
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public string UploadedFile { get; set; } = string.Empty;

        [Required]
        public DateTime SubmittedDate { get; set; }

        [Required]
        public ApprovalStatus ReviewStatus { get; set; }

        [ForeignKey(nameof(ReviewedByUser))]
        public int? ReviewedBy { get; set; }
        public User? ReviewedByUser { get; set; }

        public DateTime? ReviewedDate { get; set; }

        public string? Remarks { get; set; }
    }
}
