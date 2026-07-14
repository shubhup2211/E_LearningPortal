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
        public SubCourse SubCourse { get; set; }

        [Required]
        public string LessonTitle { get; set; } = string.Empty;

        [Required]
        public string VideoUrl { get; set; } = string.Empty;

        public string? Duration { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public List<Attachment> Attachments { get; set; } = new();
        public List<Assignment> Assignments { get; set; } = new();
        public List<Question> Questions { get; set; } = new();
        public List<MCQResult> MCQResults { get; set; } = new();
        public List<LessonProgress> LessonProgresses { get; set; } = new();
    }
}
