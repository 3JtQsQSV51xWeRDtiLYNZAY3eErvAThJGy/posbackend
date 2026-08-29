using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class OrderType
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool AffectsStock { get; set; }
        public bool RequiresPaymentFirst { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}