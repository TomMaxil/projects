using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjInventroy.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Column("prod_name", TypeName ="Varchar(20)")]
        [Required(ErrorMessage ="Name must be required")]
        [StringLength(maximumLength:20, MinimumLength =3,ErrorMessage ="3 charachter must be required")]
        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Column("prod_price", TypeName ="Decimal(30)")]
        [Required(ErrorMessage ="Prcie must be rquired")]
        public decimal Price { get; set; }

        [Column("prod_Qty", TypeName ="int")]
        [Required(ErrorMessage ="StockQty must be required")]
        public int StockQty { get; set; }

        [Column("prod_MinQty", TypeName ="int")]
        [Required(ErrorMessage ="MinStockQty must be required")]
        public int MinStockQty { get; set; }
    }
}
