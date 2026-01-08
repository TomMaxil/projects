using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjInventroy.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Column("sup_name", TypeName ="Varchar(20)")]
        [Required(ErrorMessage ="Name must be required")]
        [StringLength(maximumLength:20, MinimumLength =3,ErrorMessage ="3 charachter must be required")]
        public string Name { get; set; }

        [Column("sup_phone")]
        [Required]
        [Phone]
        public string Phone { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
        = new List<Purchase>();
    }
}
