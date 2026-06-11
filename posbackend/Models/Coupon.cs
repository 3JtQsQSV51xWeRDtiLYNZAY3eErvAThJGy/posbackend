using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Coupon
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Code { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }

    public int? DiscountPercentage { get; set; }

    public DateTime ExpiryDate { get; set; }

    public int UsageLimit { get; set; }

    public int UsedCount { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
