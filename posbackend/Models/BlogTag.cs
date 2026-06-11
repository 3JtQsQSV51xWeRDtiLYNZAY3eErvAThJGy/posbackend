using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class BlogTag
{
    [Key]
    public int Id { get; set; }

    public int BlogId { get; set; }

    [ForeignKey("BlogId")]
    public Blog? Blog { get; set; }

    [Required]
    public string Tag { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
