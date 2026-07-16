using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Authentication;
using ELearning.Models.Courses;

namespace ELearning.Models.Learning
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public int Stars { get; set; }

    }
}
