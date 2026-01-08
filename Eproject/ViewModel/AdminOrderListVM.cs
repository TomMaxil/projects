using Eproject.Models;
using System.Collections;

namespace Eproject.ViewModel
{
    public class AdminOrderListVM
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime DateOfPuchase { get; set; }

        
    }
}
