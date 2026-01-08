using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [NotMapped]
        [Compare("PasswordHash")]
        public string ConfirmPassword { get; set; }

        [MaxLength(20)]
        public string Role { get; set; } = "Customer";

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
