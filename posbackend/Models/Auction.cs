using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Auction
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal StartPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? CurrentBid { get; set; }

    public Guid? HighestBidderId { get; set; }

    [ForeignKey("HighestBidderId")]
    public User? HighestBidder { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<AuctionBid> Bids { get; set; } = new List<AuctionBid>();
}
