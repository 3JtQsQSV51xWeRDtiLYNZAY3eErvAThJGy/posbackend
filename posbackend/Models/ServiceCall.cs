using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class ServiceCall
{
    [Key]
    public int Id { get; set; }

    public int ServiceId { get; set; }

    [ForeignKey("ServiceId")]
    public Service? Service { get; set; }

    public Guid CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public User? Customer { get; set; }

    public int? StaffId { get; set; }

    [ForeignKey("StaffId")]
    public Staff? AssignedStaff { get; set; }

    public string? Description { get; set; }

    [Required]
    public string Status { get; set; } = "Pending";

    public DateTime ScheduledDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
