using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string CompanyName { get; set; }
        public string Settings { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}