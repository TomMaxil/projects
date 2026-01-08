using Eproject.Models;

namespace Eproject.ViewModel
{
    public class SearchVM
    {
        public string Keyword { get; set; }
        public List<Product> Products { get; set; }

        public int Currentpage { get; set; }

        public int Totalpages { get; set; }

        public List<Category> Categories { get; set; }

        public List<Brand> Brands { get; set; }
        
        public Guid? CategoryId { get; set; }

        public Guid? BrandId { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public string SortBy { get; set; }

    }
}
