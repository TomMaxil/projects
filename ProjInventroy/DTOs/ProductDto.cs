namespace ProjInventroy.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int StockQty { get; set; }

        public int MinStockQty { get; set; }

        public decimal Price { get; set; }
    }
}
