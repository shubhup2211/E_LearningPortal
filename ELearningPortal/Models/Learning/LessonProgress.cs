using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Models.Authentication;
using ELearning.Models.Courses;

namespace ELearning.Models.Learning
{
    public class LessonProgress
    {
        [Key]
        public int ProgressId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey(nameof(Lesson))]
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        public bool? VideoCompleted { get; set; }

        public bool? MCQCompleted { get; set; }

        public bool? AssignmentSubmitted { get; set; }

        public DateTime? CompletedDate { get; set; }
    }
}
