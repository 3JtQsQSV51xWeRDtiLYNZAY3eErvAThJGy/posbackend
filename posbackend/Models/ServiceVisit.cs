using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class ServiceVisit
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public int? CustomerId { get; set; }
        public int ProductId { get; set; }
        public int? StaffUserId { get; set; }
        public int? ResourceId { get; set; }
        public string VisitType { get; set; }
        public string Status { get; set; }
        public string WalkInName { get; set; }
        public DateTime? ScheduledStartAt { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public DateTime? ActualStartAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Note { get; set; }
        public int? VehicleId { get; set; }
        public int? PetId { get; set; }
        public int? PackageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}