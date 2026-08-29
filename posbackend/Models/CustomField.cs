using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class CustomField
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string EntityType { get; set; }
        public string FieldKey { get; set; }
        public string Type { get; set; }
        public string Config { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}