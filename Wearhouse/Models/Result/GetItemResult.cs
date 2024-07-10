using System.ComponentModel.DataAnnotations;

namespace Wearhouse.Models.Result
{
    public class GetItemResult
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int UserId { get; set; }
    }
}
