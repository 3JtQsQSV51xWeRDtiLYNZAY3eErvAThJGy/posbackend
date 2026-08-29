using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class ClassAttendance
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int StudentCustomerId { get; set; }
        public DateTime AttendedAt { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}