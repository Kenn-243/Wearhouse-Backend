using System.ComponentModel.DataAnnotations;

namespace Wearhouse.Models.Request
{
    public class CreateUserRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
