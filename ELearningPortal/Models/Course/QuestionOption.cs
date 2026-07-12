using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;

namespace ELearning.Models.Courses
{
    public class QuestionOption
    {
        [Key]
        public int OptionId { get; set; }

        [Required]
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public Question? Question { get; set; }

        [Required]
        [StringLength(255)]
        public string OptionText { get; set; } = null!;

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
