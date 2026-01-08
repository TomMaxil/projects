using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjInventroy.Models
{
    public class PurchaseItem
    {
        [Key]
        public int PurchaseItemId { get; set; }

        [ForeignKey("PurchaseId")]
        public int PurchaseId { get; set; }

        public Purchase Purchase { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Column("purI_qty", TypeName ="int")]
        [Required(ErrorMessage ="Quantity must be required")]
        public int Quantity { get; set; }

        [Column("purI_costprice", TypeName ="Decimal")]
        [Required(ErrorMessage ="Cost Price must be required")]
        public decimal CostPrice { get; set; }
    }
}
