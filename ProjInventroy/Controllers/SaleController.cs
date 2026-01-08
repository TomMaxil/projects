using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjInventroy.Models;
using ProjInventroy.Services.Inventroy;
using ProjInventroy.ViewModels;

namespace ProjInventroy.Controllers
{
    public class SaleController : Controller
    {
        private readonly eCommerceContext _context;
        private readonly IInventroyService _inventoryService;
        private readonly InventoryService _inventoryServices;

        public SaleController(
            eCommerceContext context,
            IInventroyService inventoryService,
            InventoryService inventoryServices)
        {
            _context = context;
            _inventoryService = inventoryService;
            _inventoryServices = inventoryServices;
        }

        public async Task<IActionResult> Index()
        {
            var sale = await _context.tbl_sale
                .Include(s => s.SaleItmes)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
            return View(sale);
        }

        public IActionResult Create()
        {
            var vm = new SaleCreateVM
            {
                SaleDate = DateTime.Now,
                Items = new List<SaleItemVM>
                {
                    new SaleItemVM()
                },
                Products = _context.tbl_product
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.Name
                })
                .ToList()
            };
            return View(vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(SaleCreateVM model)
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
                return View(model);
            }

            using var transiction = await _context.Database.BeginTransactionAsync();

            try
            {
                var sale = new Sale
                {
                    SaleDate = DateTime.Now,
                    SaleItmes = new List<SaleItem>()
                };

                foreach (var item in model.Items)
                {
                    var product = await _context.tbl_product.FirstOrDefaultAsync(p => p.ProductId == item.ProductId);
                    if (product == null)
                    {
                        throw new Exception("Product Not Found");
                    }

                    if (product.StockQty < item.Quantity)
                    {
                        throw new Exception($"Not Enough Stock for {product.Name}");
                    }

                    product.StockQty -= item.Quantity;

                     await _inventoryServices.NotifyLowStockAsync(
                            product.Name,
                            product.StockQty
                            );
                    

                    await _inventoryService.HandleLowStockAsync(product.ProductId);

                    sale.SaleItmes.Add(new SaleItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        SalePrice = item.SalePrice
                    });
                }

                sale.TotalAmount = sale.SaleItmes
                    .Sum(x => x.Quantity * x.SalePrice);

                _context.tbl_sale.Add(sale);
                await _context.SaveChangesAsync();
                await transiction.CommitAsync();
                return RedirectToAction("Index", "Sale");
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

                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var sale = await _context.tbl_sale
                .Include(s => s.SaleItmes)
                .ThenInclude(s => s.Product)
                .FirstOrDefaultAsync(s => s.SaleId == id);

            if(sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }
    }
}
