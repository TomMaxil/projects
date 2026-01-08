using Eproject.Helpers;
using Eproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{
    [AuthorizeUser("Admin")]
    public class CategoryController : Controller
    {
        private readonly eCommerceContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(eCommerceContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            var category = _context.tbl_category.ToList();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category cat,IFormFile CategoryImage)
        {
            string filename =  Path.GetFileName(CategoryImage.FileName);
            string path = Path.Combine(_env.WebRootPath, "images", filename);
            FileStream fs = new FileStream(path, FileMode.Create);
            CategoryImage.CopyTo(fs);

            cat.CategoryImage = filename;
            _context.tbl_category.Add(cat);
            _context.SaveChanges();
            TempData["success"] = "Category Added Successfully";
            return RedirectToAction("Index", "Category");
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var category = _context.tbl_category.Find(id);

            if (category == null)
            {
                TempData["error"] = "Category is not found.";
                return RedirectToAction("Index", "Category");
            }

            var isCategoryUsed = _context.tbl_products.Any(p => p.CategoryId == id);

            if (isCategoryUsed)
            {
                TempData["error"] = "Category could not be Deleted, becuase it's used in Product";
                return RedirectToAction("Index", "Category");
            }

            _context.tbl_category.Remove(category);
            _context.SaveChanges();
            TempData["catalert"] = "Category Deleted Succesfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
