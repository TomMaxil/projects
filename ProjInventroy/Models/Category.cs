using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjInventroy.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column("cat_name", TypeName = "Varchar(20)")]
        [Required(ErrorMessage ="Name must be required")]
        [StringLength(maximumLength:20, MinimumLength =3,ErrorMessage ="3 charachter must be required")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
