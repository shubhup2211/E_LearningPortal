using ELearning.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Models.Courses
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [ForeignKey(nameof(Lesson))]
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;

        [Required]
        public string QuestionText { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        [Required]
        public Status Status { get; set; }

        // Navigation properties
        public List<QuestionOption> QuestionOptions { get; set; } = new();
    }
}
