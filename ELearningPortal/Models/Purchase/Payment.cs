using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Authentication;

namespace ELearning.Models.Purchases
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [StringLength(100)]
        public string TransactionId { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string GatewayName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string InvoiceNumber { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = null!;

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        // Navigation properties
        public ICollection<UserCourse>? UserCourses { get; set; }
        public ICollection<UserSubCourse>? UserSubCourses { get; set; }
        public ICollection<UserSubscription>? UserSubscriptions { get; set; }
    }
}
