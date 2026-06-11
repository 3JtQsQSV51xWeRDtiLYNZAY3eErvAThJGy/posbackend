using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class ServiceResource
{
    [Key]
    public int Id { get; set; }

    public int ServiceId { get; set; }

    [ForeignKey("ServiceId")]
    public Service? Service { get; set; }

    [Required]
    public string ResourceName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int AvailableQuantity { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
