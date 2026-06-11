using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class PurchaseHistory
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    public int? ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    public int OrderId { get; set; }

    [ForeignKey("OrderId")]
    public Order? Order { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
