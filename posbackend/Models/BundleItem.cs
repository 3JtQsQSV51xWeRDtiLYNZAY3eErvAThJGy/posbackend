using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class BundleItem
    {
        [Key]
        public int Id { get; set; }
        public int BundleProductId { get; set; }
        public int ComponentVariantId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}