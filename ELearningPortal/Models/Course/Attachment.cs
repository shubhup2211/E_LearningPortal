using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Models.Courses
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }

        [Required]
        [ForeignKey(nameof(Lesson))]
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }
    }
}
