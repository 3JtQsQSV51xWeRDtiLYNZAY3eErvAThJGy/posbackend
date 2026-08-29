using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class StockSnapshot
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int LocationId { get; set; }
        public int VariantId { get; set; }
        public decimal CurrentQty { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}