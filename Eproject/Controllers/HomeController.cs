using System.Diagnostics;
using System.Threading.Tasks;
using Eproject.Helpers;
using Eproject.Models;
using Eproject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Eproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly eCommerceContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(eCommerceContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }

        
        public IActionResult Index()
        {
            
            var randomCats = _context.tbl_category
                .OrderBy(c => Guid.NewGuid())
                .Take(6)
                .ToList();

            
            var selectedCategoryIds = new List<Guid>
    {
        Guid.Parse("F5BE658C-0848-4C10-E6C6-08DE29CC0108"),
        Guid.Parse("A9F6D6DC-96CB-40C0-E6C7-08DE29CC0108"),
        Guid.Parse("E62486CD-09FC-424E-E6C8-08DE29CC0108"),
        Guid.Parse("786CDCA4-382E-43F8-E6CA-08DE29CC0108")
    };

            var categoryProducts = new Dictionary<string, List<Product>>();

            foreach (var catId in selectedCategoryIds)
            {
                var category = _context.tbl_category.FirstOrDefault(c => c.id == catId);

                if (category != null)
                {
                    var prods = _context.tbl_products
                        .Where(p => p.CategoryId == catId)
                        .Take(6)
                        .ToList();

                    categoryProducts.Add(category.CategoryName, prods);
                }
            }

            var randomBrands = _context.tbl_brand
                .OrderBy(r => Guid.NewGuid())
                .Take(8)
                .ToList();


            var vm = new HomeVM
            {
                RandomCategories = randomCats,
                CategoryProducts = categoryProducts,
                RandomBrands = randomBrands
            };


            return View(vm);
        }

        [HttpGet]
        [Route("/Products/AllProducts")]
        public  IActionResult AllProducts(int page = 1)
        {
            int pagesize = 30;

            if (pagesize < 1) page = 1;

            var query =  _context.tbl_products.Include(p => p.Category).OrderBy(p => p.ProductName);

            var totalproducts = query.Count();

            var productsonpage = query
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToList();

            var vm = new AllProductsVM
            {
                Products = productsonpage,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalproducts / (double)pagesize)

            };
 
            return View(vm);
          }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
