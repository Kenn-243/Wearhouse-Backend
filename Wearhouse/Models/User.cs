using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Wearhouse.Models
{
    public class User
    {
        [Key]
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
