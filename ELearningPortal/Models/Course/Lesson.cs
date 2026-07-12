using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Learning;

namespace ELearning.Models.Courses
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }

        [Required]
        [ForeignKey(nameof(SubCourse))]
        public int SubCourseId { get; set; }
        public SubCourse? SubCourse { get; set; }

        [Required]
        [StringLength(150)]
        public string LessonTitle { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string VideoURL { get; set; } = null!;

        public int? Duration { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public ICollection<Attachment>? Attachments { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<MCQResult>? MCQResults { get; set; }
        public ICollection<LessonProgress>? LessonProgresses { get; set; }
    }
}
