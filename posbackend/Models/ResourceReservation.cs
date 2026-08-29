using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class ResourceReservation
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int ResourceId { get; set; }
        public int? VariantId { get; set; }
        public int? VisitId { get; set; }
        public DateTime ReservedStartAt { get; set; }
        public DateTime ReservedEndAt { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}