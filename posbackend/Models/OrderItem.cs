using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int VariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Tax { get; set; }
        public string ItemCategory { get; set; }
        public int? StaffUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}