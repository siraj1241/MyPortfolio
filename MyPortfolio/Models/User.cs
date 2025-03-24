using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // e.g., "Admin", "User"
    }
}
