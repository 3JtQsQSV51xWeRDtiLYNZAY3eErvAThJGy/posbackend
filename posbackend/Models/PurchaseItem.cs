using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class PurchaseItem
    {
        [Key]
        public int Id { get; set; }
        public int PoId { get; set; }
        public int VariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal ReceivedQty { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}