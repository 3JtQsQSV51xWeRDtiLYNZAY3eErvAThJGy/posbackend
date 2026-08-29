using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class Quotation
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public string QuoteNumber { get; set; }
        public decimal PartsSubtotal { get; set; }
        public decimal LaborSubtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}