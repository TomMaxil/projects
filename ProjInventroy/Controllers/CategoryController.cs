using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjInventroy.Models;

namespace ProjInventroy.Controllers
{
    public class CategoryController : Controller
    {
        private readonly eCommerceContext _context;

        public CategoryController(eCommerceContext context)
        {
            this._context = context;
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
        public IActionResult Create(Category cat)
        {
            _context.tbl_category.Add(cat);
            _context.SaveChanges();

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int id)
        {
            var category = _context.tbl_category.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category cat)
        {
            _context.tbl_category.Update(cat);
            _context.SaveChanges();
            return RedirectToAction("Index","Category");
        }

        public IActionResult Delete(int id)
        {
            var cats = _context.tbl_category.Find(id);
            _context.tbl_category.Remove(cats);
            _context.SaveChanges();
            return RedirectToAction("Index","Category");
        }
    }
}
