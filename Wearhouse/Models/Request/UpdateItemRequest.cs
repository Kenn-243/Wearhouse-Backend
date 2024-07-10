using System.ComponentModel.DataAnnotations;

namespace Wearhouse.Models.Request
{
    public class UpdateItemRequest
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
    }
}
