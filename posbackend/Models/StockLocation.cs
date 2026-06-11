using System.ComponentModel.DataAnnotations;

namespace posbackend.Models;

public class StockLocation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Address { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
