using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models;

public class PurchaseInvoice
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    [ForeignKey("OrderId")]
    public Order? Order { get; set; }

    [Required]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required]
    public string Status { get; set; } = "Pending";

    public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

    public DateTime? PaidDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
