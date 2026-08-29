using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class StaffService
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal? CustomPrice { get; set; }
        public string CommissionType { get; set; }
        public decimal? CommissionValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}