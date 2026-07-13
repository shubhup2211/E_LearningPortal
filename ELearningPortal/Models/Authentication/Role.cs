using System.ComponentModel.DataAnnotations;

namespace ELearning.Models.Authentication
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;

        public List<User>? Users { get; set; }
    }
}
