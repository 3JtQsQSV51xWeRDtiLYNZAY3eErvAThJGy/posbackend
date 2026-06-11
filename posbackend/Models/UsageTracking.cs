using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class UsageTracking
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Required]
    public string ActionType { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int? ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
