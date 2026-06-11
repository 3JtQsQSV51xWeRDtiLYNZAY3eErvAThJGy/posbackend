using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Staff
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Position { get; set; }

    public string? Department { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ServiceCall> ServiceCalls { get; set; } = new List<ServiceCall>();
}
