using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjInventroy.Models;

namespace ProjInventroy.Controllers
{
    public class SupplierController : Controller
    {
        readonly private eCommerceContext _context;

        public SupplierController(eCommerceContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            var supp = await _context.tbl_supplies.ToListAsync();
            return View(supp);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.tbl_supplies.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Supplier");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sup = await _context.tbl_supplies.FindAsync(id);
            if(sup == null)
            {
                return NotFound();
            }
            return View(sup);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.tbl_supplies.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Supplier");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supi = await _context.tbl_supplies.FindAsync(id);
            _context.tbl_supplies.Remove(supi);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Supplier");
        }
    }
}
