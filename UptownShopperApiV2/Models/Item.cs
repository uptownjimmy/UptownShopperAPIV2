using System.ComponentModel.DataAnnotations;

namespace UptownShopperApiV2.Models
{
    public class Item : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Item_Type { get; set; }
        public bool Active { get; set; }
        public string Notes { get; set; }
        public string Created_By { get; set; }
        public string Modified_By { get; set; }
    }
}