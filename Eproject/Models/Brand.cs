using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class Brand
    {
        [Key]
        public Guid id { get; set; }

        [Required, MaxLength(100)]
        [Column("Brand_Name", TypeName ="Varchar(30)")]
        public string BrandName { get; set; }

        [MaxLength(300)]
        [Column("Brand_Image", TypeName ="Varchar(255)")]
        public string? BrandImage { get; set; }
    }
}
