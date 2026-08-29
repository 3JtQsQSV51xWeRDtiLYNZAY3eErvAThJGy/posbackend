using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class ClassEnrollment
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int StudentCustomerId { get; set; }
        public int? OrderId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}