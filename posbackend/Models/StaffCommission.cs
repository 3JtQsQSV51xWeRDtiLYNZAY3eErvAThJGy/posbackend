using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class StaffCommission
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public int? OrderItemId { get; set; }
        public int? VisitId { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal RateUsed { get; set; }
        public string Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}