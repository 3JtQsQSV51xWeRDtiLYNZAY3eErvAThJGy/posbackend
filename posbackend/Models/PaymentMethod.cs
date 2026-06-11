using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class PaymentMethod
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Required]
    public string MethodType { get; set; } = string.Empty;

    public string? CardNumber { get; set; }

    public string? CardHolder { get; set; }

    public string? ExpiryDate { get; set; }

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
