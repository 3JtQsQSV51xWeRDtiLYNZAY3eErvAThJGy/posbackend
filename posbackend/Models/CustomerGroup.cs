using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class CustomerGroup
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public decimal DefaultDiscountPct { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}