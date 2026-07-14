using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELearning.Enums;
using ELearning.Models.Authentication;

namespace ELearning.Models.Purchases
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        [Required]
        public string PlanName { get; set; } = null!;

        [Required]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int ValidityDays { get; set; }

        [Required]
        public ApprovalStatus ApprovalStatus { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public List<SubscriptionCourse> SubscriptionCourses { get; set; } = new();
        public List<UserSubscription> UserSubscriptions { get; set; } = new();
    }
}
