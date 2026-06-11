using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Subscription
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    public int SubscriptionPlanId { get; set; }

    [ForeignKey("SubscriptionPlanId")]
    public SubscriptionPlan? Plan { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    public string Status { get; set; } = "Active";

    public bool AutoRenew { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
