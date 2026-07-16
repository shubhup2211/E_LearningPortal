using ELearning.Enums;
using ELearning.Models.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningPortal.Models.Authentication
{
    public class UserViewModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        public Gender? Gender { get; set; }

        [Required]
        public IFormFile? ProfileImage { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }
    }
}
