using System.ComponentModel.DataAnnotations;

namespace Wearhouse.Models.Request
{
    public class CreateItemRequest
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
