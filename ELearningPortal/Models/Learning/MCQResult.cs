using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Models.Authentication;
using ELearning.Models.Courses;

namespace ELearning.Models.Learning
{
    public class MCQResult
    {
        [Key]
        public int ResultId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey(nameof(Lesson))]
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int TotalMarks { get; set; }

        [Required]
        public bool IsPassed { get; set; }
    }
}
