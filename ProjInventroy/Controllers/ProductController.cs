using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjInventroy.Models;

namespace ProjInventroy.Controllers
{
    public class ProductController : Controller
    {
        private readonly eCommerceContext _context;

        public ProductController(eCommerceContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var products = _context.tbl_product.ToList();

            return View(products);
        }

        public IActionResult Add()
        {
            var category = _context.tbl_category.ToList();
            ViewBag.categories = new SelectList(category, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product prod)
        {
            _context.tbl_product.Add(prod);
            _context.SaveChanges();
            return RedirectToAction("Index","Product");
        }

        public IActionResult Edit(int id)
        {
            var product = _context.tbl_product.Find(id);
            var category = _context.tbl_category.ToList();
            ViewBag.categories = new SelectList(category, "CategoryId", "Name");
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product prod)
        {
            _context.tbl_product.Update(prod);
            _context.SaveChanges();
            return RedirectToAction("Index","Product");
        }

        public IActionResult Delete(int id)
        {
            var prod = _context.tbl_product.Find(id);
            _context.tbl_product.Remove(prod);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
    }
}
