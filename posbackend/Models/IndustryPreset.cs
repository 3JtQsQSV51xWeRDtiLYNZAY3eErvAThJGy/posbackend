using System.ComponentModel.DataAnnotations;

namespace posbackend.Models;

public class IndustryPreset
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? CompanyName { get; set; }

    public string? IndustryType { get; set; }

    public string? DefaultConfig { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
