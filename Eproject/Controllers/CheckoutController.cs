using Eproject.Helpers;
using Eproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{
    [AuthorizeUser("Customer")]
    public class CheckoutController : Controller
    {
        private readonly eCommerceContext _context;

        public CheckoutController(eCommerceContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var userIdStr = HttpContext.Session.GetString("Userid");
            if (userIdStr == null)
                return RedirectToAction("Login", "User");

            Guid userId = Guid.Parse(userIdStr);

            var customer = _context.tbl_customer.FirstOrDefault(c => c.UserId == userId);
            if (customer == null)
                return RedirectToAction("Login", "User");

            var cart = _context.tbl_cart.FirstOrDefault(c => c.CustomerId == customer.id);
            if (cart == null)
                return View("Empty");

            var items = _context.tbl_cartitem
                            .Where(ci => ci.CartId == cart.CartId)
                            .Include(ci => ci.Product)
                            .ToList();

            if (!items.Any())
                return View("Empty");

            decimal total = items.Sum(i => i.Quantity * i.Product.unitCost);

            ViewBag.Customer = customer;
            ViewBag.Total = total;
            return View(items);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(string Street, string City, string State,
                                        string Country, string Mobile, string PaymentMethod,
                                        string CardNumber, string CardExpiry)
        {
            var userIdStr = HttpContext.Session.GetString("Userid");
            if (userIdStr == null)
                return RedirectToAction("Login", "User");

            Guid userId = Guid.Parse(userIdStr);

            var customer = _context.tbl_customer.FirstOrDefault(c => c.UserId == userId);
            if (customer == null)
                return RedirectToAction("Login", "User");

            var cart = _context.tbl_cart.FirstOrDefault(c => c.CustomerId == customer.id);
            if (cart == null)
                return RedirectToAction("Index", "Home");

            var items = _context.tbl_cartitem
                        .Where(ci => ci.CartId == cart.CartId)
                        .Include(ci => ci.Product)
                        .ToList();

            if (!items.Any())
                return RedirectToAction("Index", "Home");

            decimal total = items.Sum(i => i.Quantity * i.Product.unitCost);

           
            customer.Street = Street;
            customer.City = City;
            customer.State = State;
            customer.Country = Country;
            customer.Mobile = Mobile;
            customer.CreditCardNumber = CardNumber;
            customer.CreditCardExpiry = CardExpiry;

            _context.tbl_customer.Update(customer);
            _context.SaveChanges();

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = customer.id,
                TotalAmount = total,
                PaymentStatus = PaymentMethod == "COD" ? "Pending" : "Paid"
            };

            _context.tbl_order.Add(order);
            _context.SaveChanges();

            foreach (var item in items)
            {
                _context.tbl_orderitem.Add(new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitCost = item.Product.unitCost,
                    Total = item.Product.unitCost * item.Quantity
                });
            }

            _context.SaveChanges();

            _context.tbl_cartitem.RemoveRange(items);
            _context.SaveChanges();

            return RedirectToAction("Success", new { id = order.OrderId });
        }

        public IActionResult Success(Guid id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
