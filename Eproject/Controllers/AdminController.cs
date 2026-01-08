using Eproject.Helpers;
using Eproject.Models;
using Eproject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{
    [AuthorizeUser("Admin")]
    public class AdminController : Controller
    {
        readonly private eCommerceContext _context;

        public AdminController(eCommerceContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var model = new AdminDashboardVM
            {
                TotalProducts = _context.tbl_products.Count(),
                TotalOrders = _context.tbl_order.Count(),
                TotalCustomers = _context.tbl_customer.Count(),

                RecentOrders = _context.tbl_order
                .OrderByDescending(o => o.DateOfPurchase)
                .Take(5)
                .Select(o => new RecentOrderVM
                {
                    OrderId = o.OrderId,
                    PaymentStatus = o.PaymentStatus,
                    DateOfPurchase = o.DateOfPurchase
                })
                .ToList()

            };
            return View(model);
        }

        public IActionResult OrderList()
        {
            var order = _context.tbl_order
                .Include(o => o.Customer)
                .Select(o => new AdminOrderListVM
                {
                    OrderId = o.OrderId,
                    CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                    TotalAmount = o.TotalAmount,
                    PaymentStatus = o.PaymentStatus,
                    DateOfPuchase = o.DateOfPurchase
                })
                .ToList();
            return View(order);
        }

        public IActionResult UpdateOrder(Guid id, string status)
        {
            var order = _context.tbl_order.FirstOrDefault(or => or.OrderId == id);
            if(order == null)
            {
                return NotFound();
            }

            order.PaymentStatus = status;

            _context.SaveChanges();
            return RedirectToAction("OrderList","Admin");
        }

        public IActionResult Youser()
        {
            var users = _context.users
                .OrderByDescending(u => u.CreatedAt)
                .ToList();
            return View(users);
        }

        public IActionResult UserDetails(Guid id)
        {
            var users = _context.users.FirstOrDefault(u => u.UserId == id);
            if(users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        public IActionResult UserDelete(Guid id)
        {
            var users = _context.users.FirstOrDefault(u => u.UserId == id);
            if(users == null)
            {
                return NotFound();
            }

            _context.users.Remove(users);
            _context.SaveChanges();
            return RedirectToAction("Youser","Admin");
        }
    }
}
