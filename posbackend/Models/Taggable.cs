using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class Taggable
    {
        [Key]
        public int Id { get; set; }
        public int TagId { get; set; }
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}