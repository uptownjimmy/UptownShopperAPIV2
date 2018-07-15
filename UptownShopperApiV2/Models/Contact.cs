using System.ComponentModel.DataAnnotations;

namespace UptownShopperApiV2.Models
{
    public class Contact : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Display_Name { get; set; }
        public string User_Id { get; set; }
        public string Email { get; set; }
        public string Mobile_Phone { get; set; }
        public string Home_Phone { get; set; }
        public string Work_Phone { get; set; }
        public string Fax { get; set; }
        public int Department { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public string Created_By { get; set; }
        public string Modified_By { get; set; }
    }
}