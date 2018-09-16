using System.ComponentModel.DataAnnotations;

namespace UptownShopperApiV2.Models
{
    public class Item : BaseEntity
    {
//        public Item(object storeNames)
//        {
//            Store_Names = storeNames;
//        }

        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Item_Type { get; set; }
        public bool Active { get; set; }
        public string Notes { get; set; }
        public object Store_Names { get; set; }
        public string Created_By { get; set; }
        public string Modified_By { get; set; }
    }
}