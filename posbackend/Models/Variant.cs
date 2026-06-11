using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Variant
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Value { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? PriceModifier { get; set; }

    public int Stock { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
