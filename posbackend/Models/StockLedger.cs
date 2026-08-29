using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class StockLedger
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int VariantId { get; set; }
        public int LocationId { get; set; }
        public string MovementType { get; set; }
        public decimal Quantity { get; set; }
        public decimal BalanceAfter { get; set; }
        public string ReferenceType { get; set; }
        public int? ReferenceId { get; set; }
        public string Note { get; set; }
        public DateTime OccurredAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}