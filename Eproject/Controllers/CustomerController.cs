using Eproject.Helpers;
using Eproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{
    [AuthorizeUser("Customer")]
    public class CustomerController : Controller
    {
        private readonly eCommerceContext _context;

        public CustomerController(eCommerceContext context)
        {
            this._context = context;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult AddCart(Guid productId)
        {
           
            var UserIdStr = HttpContext.Session.GetString("Userid");
            if (UserIdStr == null)
                return RedirectToAction("Login", "User");

            Guid userid = Guid.Parse(UserIdStr);

            
            var customer = _context.tbl_customer.FirstOrDefault(c => c.UserId == userid);
            if (customer == null)
                return RedirectToAction("Login", "User");

           
            var product = _context.tbl_products.FirstOrDefault(p => p.id == productId);
            if (product == null)
            {
                TempData["error"] = "Product not found!";
                return RedirectToAction("Index", "Home");
            }

            
            var cart = _context.tbl_cart.FirstOrDefault(ca => ca.CustomerId == customer.id);
            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = Guid.NewGuid(),
                    CustomerId = customer.id
                };

                _context.tbl_cart.Add(cart);
                _context.SaveChanges();
            }

           
            var cartitem = _context.tbl_cartitem
                .FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

            if (cartitem != null)
            {
                cartitem.Quantity++;
            }
            else
            {
                cartitem = new CartItem
                {
                    CartItemId = Guid.NewGuid(),
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = 1
                };

                _context.tbl_cartitem.Add(cartitem);
            }

            _context.SaveChanges();

            TempData["cartsuccess"] = "Item Added to Cart!";
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Cart()
        {
            var UserIdStr = HttpContext.Session.GetString("Userid");
            if (UserIdStr == null)
                return RedirectToAction("Login", "User");

            Guid userid = Guid.Parse(UserIdStr);

           
            var customer = _context.tbl_customer.FirstOrDefault(c => c.UserId == userid);

            if (customer == null)
                return RedirectToAction("Login", "User");

           
            var cart = _context.tbl_cart.FirstOrDefault(c => c.CustomerId == customer.id);

            if (cart == null)
                return View(new List<CartItem>());


            var items = _context.tbl_cartitem
                        .Where(ci => ci.CartId == cart.CartId)
                        .Include(ci => ci.Product)
                        .ToList();

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveCartItem(Guid CartItemId)
        {
            var userIdStr = HttpContext.Session.GetString("Userid");
            if(userIdStr == null)
            {
                return RedirectToAction("Login", "User");
            }

            var cartitem = _context.tbl_cartitem.Include(ci => ci.Cart).FirstOrDefault(ci => ci.CartItemId == CartItemId);
            if(cartitem == null)
            {
                return RedirectToAction("Cart", "Customer");
            }

            Guid userid = Guid.Parse(userIdStr);

            var customer = _context.tbl_customer.FirstOrDefault(c => c.UserId == userid);
            if (customer == null || cartitem.Cart == null || cartitem.Cart.CustomerId != customer.id)
            {
                return RedirectToAction("Cart", "Customer");
            }

            _context.tbl_cartitem.Remove(cartitem);
            _context.SaveChanges();

            TempData["cartsuccess"] = "Item removed.";
            return RedirectToAction("Cart", "Customer");
        }

        public IActionResult MyOrders()
        {
            var userIdStr = HttpContext.Session.GetString("Userid");
            if(userIdStr == null)
            {
                return RedirectToAction("Login", "User");
            }

            Guid userId = Guid.Parse(userIdStr);

            var customer = _context.tbl_customer.FirstOrDefault(c => c.UserId == userId);
            if(customer == null)
            {
                return RedirectToAction("Login", "User");
            }

            var order = _context.tbl_order.Where(o => o.CustomerId == customer.id).OrderByDescending(o => o.DateOfPurchase).ToList();

            return View(order);
        }

        public IActionResult OrderDetails(Guid id)
        {
            var userIdStr = HttpContext.Session.GetString("Userid");
            if(userIdStr == null)
            {
                return RedirectToAction("login", "User");
            }

            Guid userId = Guid.Parse(userIdStr);

            var customer = _context.tbl_customer.FirstOrDefault(c => c.UserId == userId);
            if(customer == null)
            {
                return RedirectToAction("Login", "User");
            }

            var order = _context.tbl_order.FirstOrDefault(o => o.OrderId == id && o.CustomerId == customer.id);
            if(order == null)
            {
                return RedirectToAction("MyOrders", "Customer");
            }

            var items = _context.tbl_orderitem.Where(oi => oi.OrderId == id).Include(p => p.Product).ToList();

            ViewBag.Order = order;
            return View(items);
        }
    }

}
