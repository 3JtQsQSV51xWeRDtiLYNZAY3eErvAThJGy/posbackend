using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class ServiceQueue
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public DateTime QueueDate { get; set; }
        public int NextNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}