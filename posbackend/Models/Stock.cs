using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Stock
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    public int StockLocationId { get; set; }

    [ForeignKey("StockLocationId")]
    public StockLocation? StockLocation { get; set; }

    public int Quantity { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
