using ELearning.Enums;
using ELearning.Models.Courses;
using ELearning.Models.Learning;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningPortal.Models.Course
{
    public class LessonView
    {
            [Key]
            public int LessonId { get; set; }

            [Required(ErrorMessage = "Required Field")]
            public int SubCourseId { get; set; }

            [Required(ErrorMessage = "Required Field")]
            public string LessonTitle { get; set; } = string.Empty;

            [Required(ErrorMessage = "Required Field")]
            public string VideoUrl { get; set; } = string.Empty;

            public string? Duration { get; set; }

            [Required(ErrorMessage = "Required Field")]
            public int DisplayOrder { get; set; }

            [Required(ErrorMessage = "Required Field")]
            public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<Lesson> Lessonlist { get; set; }
    }
}
