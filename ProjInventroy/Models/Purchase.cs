using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjInventroy.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Column("pur_date")]
        [Required]
        public DateTime PurchaseDate { get; set; }

        [Column("pur_Amount", TypeName ="Decimal")]
        [Required(ErrorMessage ="Total Amount must be required")]
        public decimal TotalAmount { get; set; }

        public ICollection<PurchaseItem> PurchaseItems  { get; set; }
    }
}
