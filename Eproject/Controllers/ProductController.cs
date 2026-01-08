using Eproject.Helpers;
using Eproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{

    [AuthorizeUser("Admin")]
    public class ProductController : Controller
    {
        private readonly eCommerceContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(eCommerceContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            var product = _context.tbl_products.ToList();
            return View(product);
        }

        public IActionResult Add()
        {
            var categories = _context.tbl_category.ToList();
            ViewBag.categori = new SelectList(categories, "id", "CategoryName");
            var brands = _context.tbl_brand.ToList();
            ViewBag.brand = new SelectList(brands, "id", "BrandName");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product prod, IFormFile image)
        {
            var filename = Guid.NewGuid().ToString() + Path.GetFileName(image.FileName);
            var filepath = Path.Combine(_env.WebRootPath, "images", filename);
            FileStream fs = new FileStream(filepath, FileMode.Create);
            image.CopyTo(fs);

            prod.image = filename;
            _context.tbl_products.Add(prod);
            _context.SaveChanges();
            TempData["alert"] = "Product Added Successfully";
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Edit(Guid id)
        {
            var product = _context.tbl_products.Find(id);
            var categori = _context.tbl_category.ToList();
            var brands = _context.tbl_brand.ToList();
            ViewBag.categori = new SelectList(categori, "id", "CategoryName");
            ViewBag.brand = new SelectList(brands, "id", "BrandName");
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product prod, IFormFile image)
        {
            if (image != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetFileName(image.FileName);
                var filepath = Path.Combine(_env.WebRootPath, "images", filename);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                image.CopyTo(fs);
            }
            else
            {
                var existimage = _context.tbl_products.AsNoTracking().FirstOrDefault(p => p.id == prod.id);
                prod.image = existimage.image;
            }

            _context.tbl_products.Update(prod);
            _context.SaveChanges();
            TempData["alert1"] = "Product Update Successfully";
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Details(Guid id)
        {
            var products = _context.tbl_products
            .Include(p => p.Category) 
            .Include(p => p.Brand)   
            .FirstOrDefault(p => p.id == id);

            return View(products);
        }

        public IActionResult Delete(Guid id)
        {
            var product = _context.tbl_products.Find(id);
            _context.tbl_products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
    }
}
