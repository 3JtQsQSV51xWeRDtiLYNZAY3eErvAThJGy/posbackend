using System;
using System.ComponentModel.DataAnnotations;

namespace posbackend.Models
{
    public class ItemType
    {
        [Key]
        public int Id { get; set; } // Remains int (serial4)
        public Guid TenantId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool TrackStockDefault { get; set; }
        public bool IsService { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
