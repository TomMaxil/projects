using Eproject.Helpers;
using Eproject.Models;
using Eproject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{

   
    public class ProductsController : Controller
    {
        private readonly eCommerceContext _context;

        public ProductsController(eCommerceContext context)
        {
            this._context = context;
        }
        public IActionResult ByCategory(Guid id, int page = 1)
        {
            int pageSize = 15;

            
            var category = _context.tbl_category.FirstOrDefault(c => c.id == id);
            if (category == null)
                return NotFound();

           
            var totalProducts = _context.tbl_products
                .Where(p => p.CategoryId == id)
                .Count();

           
            var products = _context.tbl_products
                .Where(p => p.CategoryId == id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            var vm = new ProductsByCategoryVM
            {
                Category = category,
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(vm);
        }

        public IActionResult Details(Guid id)
        {
            var product = _context.tbl_products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.id == id);

            if (product == null)
                return NotFound();

            
            product.Reviews = _context.tbl_productreview
                .Where(r => r.ProductId == id)
                .Include(r => r.Customer)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

           
            if (product.Reviews.Any())
                ViewBag.AvgRating = product.Reviews.Average(r => r.Rating);
            else
                ViewBag.AvgRating = 0;

          
            ViewBag.CanReview = false;

            var userIdStr = HttpContext.Session.GetString("Userid");
            if (userIdStr != null)
            {
                Guid userId = Guid.Parse(userIdStr);

                var customer = _context.tbl_customer
                    .FirstOrDefault(c => c.UserId == userId);

                if (customer != null)
                {
                    bool purchased = _context.tbl_orderitem
                        .Include(oi => oi.Order)
                        .Any(oi =>
                            oi.ProductId == id &&
                            oi.Order.CustomerId == customer.id
                        );

                    ViewBag.CanReview = purchased;
                }
            }

            return View(product);
        }


        public IActionResult Search(
        string s,
        Guid? categoryId,
        Guid? brandId,
        int minprice,
        int maxprice,
        string sortby,
        int page = 1)
        {
            if (string.IsNullOrWhiteSpace(s))
                return RedirectToAction("Index", "Home");

            int pageSize = 12;

            s = s.ToLower();

            var query = _context.tbl_products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p =>
                    p.ProductName.ToLower().Contains(s) ||
                    p.description.ToLower().Contains(s) ||
                    p.Category.CategoryName.ToLower().Contains(s) ||
                    p.Brand.BrandName.ToLower().Contains(s)
                );

            if (categoryId != null)
                query = query.Where(p => p.CategoryId == categoryId);

            if (brandId != null)
                query = query.Where(p => p.BrandId == brandId);

           
            if (minprice > 0)
                query = query.Where(p => p.unitCost >= minprice);

            if (maxprice > 0)
                query = query.Where(p => p.unitCost <= maxprice);


            switch (sortby)
            {
                case "low": query = query.OrderBy(p => p.unitCost); break;
                case "high": query = query.OrderByDescending(p => p.unitCost); break;
                case "latest": query = query.OrderByDescending(p => p.id); break;
                default: query = query.OrderBy(p => p.ProductName); break;
            }

            var totalProducts = query.Count();

            var products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new SearchVM
            {
                Keyword = s,
                Products = products,
                Currentpage = page,
                Totalpages = (int)Math.Ceiling(totalProducts / (double)pageSize),
                Categories = _context.tbl_category.ToList(),
                Brands = _context.tbl_brand.ToList(),
                CategoryId = categoryId,
                BrandId = brandId,
                MinPrice = minprice,
                MaxPrice = maxprice,
                SortBy = sortby
            };

            return View(vm);
        }

    }

}

