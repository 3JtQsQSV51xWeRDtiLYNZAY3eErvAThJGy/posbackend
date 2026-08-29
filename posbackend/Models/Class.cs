using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int CourseProductId { get; set; }
        public int? TeacherUserId { get; set; }
        public int? RoomResourceId { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}