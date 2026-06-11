using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Loyalty
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    public int Points { get; set; }

    public int TotalPoints { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
