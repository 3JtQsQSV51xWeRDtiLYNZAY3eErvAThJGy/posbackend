using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class CustomFieldValue
    {
        [Key]
        public int Id { get; set; }
        public int FieldId { get; set; }
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}