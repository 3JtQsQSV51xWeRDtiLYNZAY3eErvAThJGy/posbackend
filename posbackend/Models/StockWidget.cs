using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class StockWidget
{
    [Key]
    public int Id { get; set; }

    public int StockLocationId { get; set; }

    [ForeignKey("StockLocationId")]
    public StockLocation? StockLocation { get; set; }

    [Required]
    public string WidgetName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int WidgetType { get; set; }

    public string? Reference { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
