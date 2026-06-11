using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class Blog
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }

    public Guid AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    public User? Author { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public BlogCategory? Category { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<BlogTag> Tags { get; set; } = new List<BlogTag>();

    public ICollection<BlogComment> Comments { get; set; } = new List<BlogComment>();
}
