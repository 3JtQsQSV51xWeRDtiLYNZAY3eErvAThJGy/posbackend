using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class CustomerDiscount
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    public int DiscountId { get; set; }

    [ForeignKey("DiscountId")]
    public Discount? Discount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
