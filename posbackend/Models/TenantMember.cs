using System;
using System.ComponentModel.DataAnnotations;

namespace posbackend.Models
{
    public class TenantMember
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsOwner { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
