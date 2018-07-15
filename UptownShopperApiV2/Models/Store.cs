using System.ComponentModel.DataAnnotations;

namespace UptownShopperApiV2.Models
{
    public class Store : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Store_Type { get; set; }
        public string Created_By { get; set; }
        public string Modified_By { get; set; }
    }
}