using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjInventroy.Models;

namespace ProjInventroy.Controllers
{
    public class AdminController : Controller
    {
        private readonly eCommerceContext _context;

        public AdminController(eCommerceContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.LowStockproducts = _context.tbl_product.Count(p => p.StockQty <= p.MinStockQty);
            ViewBag.TotalCategory = _context.tbl_category.Count();
            ViewBag.TotalProducts = _context.tbl_product.Count();
            ViewBag.TotalSales = _context.tbl_sale.Count();
            ViewBag.TotalPurchases = _context.tbl_puchase.Count();
            return View();
        }

        public async Task<IActionResult> LowStock()
        {
            var lowstockproducts = await _context.tbl_product.Where(p => p.StockQty <= p.MinStockQty).OrderBy(p => p.StockQty).ToListAsync();
            return View(lowstockproducts);
        }
    }
}
