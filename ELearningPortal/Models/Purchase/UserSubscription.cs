using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Authentication;

namespace ELearning.Models.Purchases
{
    public class UserSubscription
    {
        [Key]
        public int UserSubscriptionId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey(nameof(Subscription))]
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }

        [ForeignKey(nameof(Payment))]
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public PurchaseStatus Status { get; set; }
    }
}
