using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Models.Courses;

namespace ELearning.Models.Purchases
{
    public class SubscriptionCourse
    {
        [Key]
        public int SubscriptionCourseId { get; set; }

        [Required]
        [ForeignKey(nameof(Subscription))]
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
