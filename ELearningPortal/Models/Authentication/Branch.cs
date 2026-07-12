using System.ComponentModel.DataAnnotations;
using ELearning.Enums;
using ELearning.Models.Courses;

namespace ELearning.Models.Authentication
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchName { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Location { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Pincode { get; set; } = null!;

        public Gender? Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactPerson { get; set; } = null!;

        [StringLength(255)]
        public string? ProfileImage { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; } = null!;

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        // Navigation properties
        public ICollection<User>? Users { get; set; }
        public ICollection<SubCourse>? SubCourses { get; set; }
    }
}
