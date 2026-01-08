using System.ComponentModel.DataAnnotations;

namespace ProjInventory.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQty { get; set; }

        [Range(0, int.MaxValue)]
        public int MinStockQty { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
