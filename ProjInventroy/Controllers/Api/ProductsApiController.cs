using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ProjInventory.DTOs;
using ProjInventroy.DTOs;
using ProjInventroy.Models;

namespace ProjInventroy.Controllers.Api
{
    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly eCommerceContext _context;

        public ProductsApiController(eCommerceContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.tbl_product
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    StockQty = p.StockQty,
                    MinStockQty = p.MinStockQty,
                    Price = p.Price
                })
                .ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.tbl_product.FindAsync(id);

            if(product == null)
            {
                return NotFound("Product Not Found");
            }

            var dto = new ProductDto
            {
                Id = product.ProductId,
                Name = product.Name,
                StockQty = product.StockQty,
                MinStockQty = product.MinStockQty,
                Price = product.Price
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var product = new Product
                {
                    Name = dto.Name,
                    StockQty = dto.StockQty,
                    MinStockQty = dto.MinStockQty,
                    Price = dto.Price,
                    CategoryId = dto.CategoryId
                };

                _context.tbl_product.Add(product);
                await _context.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.tbl_product.FindAsync(id);
            if (product == null)
                return NotFound("Product not found");

            product.Name = dto.Name;
            product.StockQty = dto.StockQty;
            product.MinStockQty = dto.MinStockQty;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.tbl_product.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            _context.tbl_product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
