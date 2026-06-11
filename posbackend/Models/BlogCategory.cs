using System.ComponentModel.DataAnnotations;

namespace posbackend.Models;

public class BlogCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
