using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class Product
    {
        [Key]
        public Guid id { get; set; }

        [Required, MaxLength(200)]
        [Column("prod_name", TypeName ="Varchar(20)")]
        public string ProductName { get; set; }

        [MaxLength(500)]
        [Column("prod_description", TypeName ="Varchar(220)")]
        public string description { get; set; }

        [Required]
        public int quantity { get; set; }

        [Column("prod_Unicost", TypeName = "decimal(10,2)")]
        public decimal unitCost { get; set; }

        [Column("prod_Discount", TypeName = "decimal(10,2)")]
        public decimal discount { get; set; }

        [MaxLength(300)]
        [Column("prod_image", TypeName ="Varchar(255)")]
        public string? image { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        public Brand Brand { get; set; }

        public List<ProductReview> Reviews { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
