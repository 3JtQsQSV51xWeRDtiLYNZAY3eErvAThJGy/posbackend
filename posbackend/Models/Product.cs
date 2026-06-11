using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }

    public string? ImageUrl { get; set; }

    public int? SupplierId { get; set; }

    [ForeignKey("SupplierId")]
    public Supplier? Supplier { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public ICollection<Variant> Variants { get; set; } = new List<Variant>();

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
