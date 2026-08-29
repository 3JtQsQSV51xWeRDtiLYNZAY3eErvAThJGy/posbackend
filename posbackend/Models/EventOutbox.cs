using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class EventOutbox
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string EventType { get; set; }
        public string Payload { get; set; }
        public string Status { get; set; }
        public int Attempts { get; set; }
        public DateTime? LastTriedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}