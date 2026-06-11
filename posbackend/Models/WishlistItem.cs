using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class WishlistItem
{
    [Key]
    public int Id { get; set; }

    public int WishlistId { get; set; }

    [ForeignKey("WishlistId")]
    public Wishlist? Wishlist { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
