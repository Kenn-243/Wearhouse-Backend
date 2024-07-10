using System.ComponentModel.DataAnnotations;

namespace Wearhouse.Models
{
    public class Item
    {
        [Key]
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
