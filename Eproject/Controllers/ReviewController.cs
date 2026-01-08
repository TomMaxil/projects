using Eproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{
    public class ReviewController : Controller
    {
        private readonly eCommerceContext _context;

        public ReviewController(eCommerceContext context)
        {
            this._context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Guid productId, int rating, string comment)
        {
            var userIdStr = HttpContext.Session.GetString("Userid");
            if(userIdStr == null)
            {
                return RedirectToAction("Login", "User");
            }

            Guid userId = Guid.Parse(userIdStr);

            var customer = _context.tbl_customer.FirstOrDefault(cu => cu.UserId == userId);
            if(customer == null)
            {
                return RedirectToAction("Login", "User");
            }

            bool puchased = _context.tbl_orderitem.Include(oi => oi.Order).Any(oi => oi.ProductId == productId && oi.Order.CustomerId == customer.id);
            if (!puchased)
            {
                return Unauthorized();
            }

            var existing = _context.tbl_productreview.FirstOrDefault(r => r.ProductId == productId && r.CustomerId == customer.id);
            if(existing != null)
            {
                existing.Rating = rating;
                existing.Comment = comment;
                existing.CreatedAt = DateTime.Now;
            }
            else
            {
                var review = new ProductReview
                {
                    ReviewId = Guid.NewGuid(),
                    ProductId = productId,
                    CustomerId = customer.id,
                    Rating = rating,
                    Comment = comment,
                    CreatedAt = DateTime.Now
                };

                _context.tbl_productreview.Add(review);
            }

            _context.SaveChanges();
            TempData["Done"] = "Review Submitted";
            return RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
