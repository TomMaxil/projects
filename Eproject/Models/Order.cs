using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime DateOfPurchase { get; set; } = DateTime.Now;

        [Column("Order_totalamount", TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [MaxLength(20)]
        [Column("Order_paymentstatus", TypeName ="Varchar(255)")]
        public string PaymentStatus { get; set; }
    }
}
