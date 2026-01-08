using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class ProductReview
    {
        [Key]
        public Guid ReviewId { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        [Range(1, 5)]
        [Column("Pr_rating", TypeName ="int")]
        public int Rating { get; set; }

        [MaxLength(500)]
        [Column("Pr_comment", TypeName = "Varchar(220)")]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
