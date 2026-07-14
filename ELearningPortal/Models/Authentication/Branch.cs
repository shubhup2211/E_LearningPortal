using System.ComponentModel.DataAnnotations;
using ELearning.Enums;
using ELearning.Models.Courses;
using ELearning.Models.Purchases;

namespace ELearning.Models.Authentication
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        public string BranchName { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Pincode { get; set; } = string.Empty;

        [Required]
        public string ContactPerson { get; set; } = string.Empty;

        [Required]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public List<User>? Users { get; set; } = new();
        public List<Course>? Courses { get; set; } = new();
        public List<Subscription>? Subscriptions { get; set; } = new();
    }
}
