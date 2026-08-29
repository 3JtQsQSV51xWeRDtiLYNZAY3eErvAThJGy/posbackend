using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class SubscriptionPlan
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MonthlyPrice { get; set; }
        public string Features { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}