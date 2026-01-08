using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjInventroy.Models
{
    public class SaleItem
    {
        [Key]
        public int SaleItemId { get; set; }

        [ForeignKey("SaleId")]
        public int SaleId { get; set; }

        public Sale Sale { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Column("saleitem_Qty", TypeName ="int")]
        [Required(ErrorMessage ="Quantity must be required")]
        public int Quantity { get; set; }

        [Column("saleitem_price", TypeName ="Decimal")]
        [Required]
        public decimal SalePrice { get; set; }
    }
}
