using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class CustomerPackage
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal TotalUnits { get; set; }
        public decimal RemainingUnits { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}