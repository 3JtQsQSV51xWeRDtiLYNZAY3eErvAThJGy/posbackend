using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class QuotationItem
    {
        [Key]
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public string ItemType { get; set; }
        public int? VariantId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}