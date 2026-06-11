using System.ComponentModel.DataAnnotations;

namespace posbackend.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;

    public int? CustomerGroupId { get; set; }

    [ForeignKey("CustomerGroupId")]
    public CustomerGroup? CustomerGroup { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

    public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public ICollection<AuctionBid> AuctionBids { get; set; } = new List<AuctionBid>();

    public ICollection<Blog> AuthoredBlogs { get; set; } = new List<Blog>();

    public ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    public ICollection<Loyalty> Loyalties { get; set; } = new List<Loyalty>();

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();

    public ICollection<UsageTracking> UsageTrackings { get; set; } = new List<UsageTracking>();

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
