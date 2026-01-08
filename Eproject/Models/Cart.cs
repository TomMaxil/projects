using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class Cart
    {
        [Key]
        public Guid CartId { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        
    }
}
