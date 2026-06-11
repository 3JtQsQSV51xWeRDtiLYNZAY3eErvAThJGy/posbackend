using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class PaymentProcessing
{
    [Key]
    public int Id { get; set; }

    public int PaymentId { get; set; }

    [ForeignKey("PaymentId")]
    public Payment? Payment { get; set; }

    [Required]
    public string ProcessingStatus { get; set; } = "Pending";

    public string? GatewayTransactionId { get; set; }

    public string? ErrorMessage { get; set; }

    public int AttemptCount { get; set; }

    public DateTime ProcessedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
