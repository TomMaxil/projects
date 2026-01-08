using Eproject.Helpers;
using Eproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{
    [AuthorizeUser("Admin")]
    public class BrandController : Controller
    {
        private readonly eCommerceContext _context;
        private readonly IWebHostEnvironment _env;

        public BrandController(eCommerceContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            var brand = _context.tbl_brand.ToList();
            return View(brand);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand bra, IFormFile BrandImage)
        {
            string filename = Guid.NewGuid().ToString() + Path.GetFileName(BrandImage.FileName);
            string path = Path.Combine(_env.WebRootPath, "brand", filename);
            FileStream fs = new FileStream(path, FileMode.Create);
            BrandImage.CopyTo(fs);

            bra.BrandImage = filename;
            _context.tbl_brand.Add(bra);
            _context.SaveChanges();
            TempData["brand"] = "Brand Added Successfully";
            return RedirectToAction("Index", "Brand");
        }

        public IActionResult Edit(Guid id)
        {
            var brands = _context.tbl_brand.Find(id);
            return View(brands);
        }

        [HttpPost]
        public IActionResult Edit(Brand bra, IFormFile BrandImage)
        {
           if(BrandImage != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetFileName(BrandImage.FileName);
                string path = Path.Combine(_env.WebRootPath, "brand", filename);
                FileStream fs = new FileStream(path, FileMode.Create);
                BrandImage.CopyTo(fs);
            }
            else
            {
                var existimage = _context.tbl_brand.AsNoTracking().FirstOrDefault(b => b.id == bra.id);
                bra.BrandImage = existimage.BrandImage;
            }

            

            _context.tbl_brand.Update(bra);
            _context.SaveChanges();
            TempData["brandalert"] = "Brand Update Successfully";
            return RedirectToAction("Index","Brand");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var brand = _context.tbl_brand.Find(id);
            if (brand == null)
            {
                TempData["error"] = "Brand is not found.";
                return RedirectToAction("Index", "Brand");
            }

            var isUsedBrand = _context.tbl_products.Any(p => p.BrandId == p.id);
            if (isUsedBrand)
            {
                TempData["error"] = "Brand is could not be deleted, because it's used in Product";
                return RedirectToAction("Index", "Brand");
            }

            _context.tbl_brand.Remove(brand);
            _context.SaveChanges();
            TempData["brands"] = "Brand Deleted Successfully";
            return RedirectToAction("Index","Brand");
        }
    }
}
