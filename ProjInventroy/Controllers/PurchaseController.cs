using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjInventroy.Models;
using ProjInventroy.ViewModels;

namespace ProjInventroy.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly eCommerceContext _context;

        public PurchaseController(eCommerceContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var purchase = await _context.tbl_puchase
                .Include(p => p.PurchaseItems)
                .OrderByDescending(p => p.PurchaseDate)
                .ToListAsync();
            return View(purchase);
        }
        public IActionResult Create()
        {
            var vm = new PurchaseCreateVM
            {
                PurchaseDate = DateTime.Now,
                Items = new List<PurchaseItemVM>
                {
                   new PurchaseItemVM()
                },
                Products = _context.tbl_product
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.Name
                }).ToList(),

                Suplliers = _context.tbl_supplies
                .Select(s => new SelectListItem
                {
                    Value = s.SupplierId.ToString(),
                    Text = s.Name
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                model.Products = _context.tbl_product
                    .Select(p => new SelectListItem
                    {
                        Value = p.ProductId.ToString(),
                        Text = p.Name
                    }).ToList();

                model.Suplliers = _context.tbl_supplies
                     .Select(s => new SelectListItem
                     {
                         Value = s.SupplierId.ToString(),
                         Text = s.Name
                     }).ToList();

                return View(model);
            }

            using var transiction = await _context.Database.BeginTransactionAsync();

            try { 
            var purchase = new Purchase
            {
                SupplierId = model.SupplierId,
                PurchaseDate = model.PurchaseDate,
                PurchaseItems = new List<PurchaseItem>()
            };

            foreach(var item in model.Items)
            {
                var product = await _context.tbl_product.FirstOrDefaultAsync(p => p.ProductId == item.ProductId);
                if(product == null)
                {
                    throw new Exception("Product Not Found");
                }

                product.StockQty += item.Quantity;

                purchase.PurchaseItems.Add(new PurchaseItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    CostPrice = item.CostPrice
                });
            }

            purchase.TotalAmount = purchase.PurchaseItems
                .Sum(x => x.Quantity * x.CostPrice);

               

            _context.tbl_puchase.Add(purchase);
            await _context.SaveChangesAsync();
                await transiction.CommitAsync();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                await transiction.RollbackAsync();

                model.Products = _context.tbl_product
                    .Select(p => new SelectListItem
                    {
                        Value = p.ProductId.ToString(),
                        Text = p.Name
                    }).ToList();

                model.Suplliers = _context.tbl_supplies
                    .Select(s => new SelectListItem
                    {
                        Value = s.SupplierId.ToString(),
                        Text = s.Name
                    }).ToList();

                ModelState.AddModelError("", "Something Went wrong While Saving Purchase.");
                return View(model);
            }
           
        }

        public async Task<IActionResult> Details(int id)
        {
            var purchase = await _context.tbl_puchase
                .Include(p => p.PurchaseItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(p => p.PurchaseId == id);

            if(purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }
    }
}
