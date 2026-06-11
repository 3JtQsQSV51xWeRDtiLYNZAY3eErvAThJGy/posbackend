using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class AuctionBid
{
    [Key]
    public int Id { get; set; }

    public int AuctionId { get; set; }

    [ForeignKey("AuctionId")]
    public Auction? Auction { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal BidAmount { get; set; }

    public DateTime BidTime { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
