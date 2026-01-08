using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class Category
    {
        [Key]
        public Guid id { get; set; }

        [Required, MaxLength(20)]
        [Column("Category_Name", TypeName ="Varchar(20)")]
        public string CategoryName { get; set; }

        [MaxLength(220)]
        [Column("Category_Desc", TypeName ="Varchar(220)")]
        public string Description { get; set; }

        [MaxLength(255)]
        [Column("Category_Image", TypeName = "Varchar(255)")]
        public string? CategoryImage { get; set; } // category image

    }
}
