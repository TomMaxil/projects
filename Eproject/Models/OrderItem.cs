using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [Column("Orderitem_quantity", TypeName ="int")]
        public int Quantity { get; set; }

        [Column("Orderitem_unicost", TypeName = "decimal(10,2)")]
        public decimal UnitCost { get; set; }

        [Column("Orderitem_total", TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
    }
}
