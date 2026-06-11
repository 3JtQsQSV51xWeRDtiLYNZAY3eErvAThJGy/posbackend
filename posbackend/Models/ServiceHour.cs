using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class ServiceHour
{
    [Key]
    public int Id { get; set; }

    public int ServiceId { get; set; }

    [ForeignKey("ServiceId")]
    public Service? Service { get; set; }

    [Required]
    public string DayOfWeek { get; set; } = string.Empty;

    public TimeSpan OpeningTime { get; set; }

    public TimeSpan ClosingTime { get; set; }

    public bool IsOpen { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
