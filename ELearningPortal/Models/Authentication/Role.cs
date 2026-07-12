using System.ComponentModel.DataAnnotations;

namespace ELearning.Models.Authentication
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = null!;

        public ICollection<User>? Users { get; set; }
    }
}
