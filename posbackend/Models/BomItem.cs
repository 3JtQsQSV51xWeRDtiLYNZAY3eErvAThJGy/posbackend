using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class BomItem
    {
        [Key]
        public int Id { get; set; }
        public int CompositeProductId { get; set; }
        public int IngredientVariantId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}