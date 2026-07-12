using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Authentication;
using ELearning.Models.Courses;

namespace ELearning.Models.Purchases
{
    public class UserSubCourse
    {
        [Key]
        public int UserSubCourseId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey(nameof(SubCourse))]
        public int SubCourseId { get; set; }
        public SubCourse? SubCourse { get; set; }

        [ForeignKey(nameof(Payment))]
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Required]
        public PurchaseStatus Status { get; set; }
    }
}
