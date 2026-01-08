using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class CartItem
    {
        [Key]
        public Guid CartItemId { get; set; }

        [ForeignKey("Cart")]
        public Guid CartId { get; set; }

        public Cart Cart { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [Column("Cartitem_quantity", TypeName ="int")]
        public int Quantity { get; set; }
    }
}
