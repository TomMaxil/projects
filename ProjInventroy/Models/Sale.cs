using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjInventroy.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Column("sale_date")]
        public DateTime SaleDate { get; set; }

        [Column("sale_amount", TypeName ="Decimal")]
        public decimal TotalAmount { get; set; }

        public ICollection<SaleItem> SaleItmes { get; set; }
    }
}
