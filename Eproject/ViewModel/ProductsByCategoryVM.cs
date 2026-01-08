using Eproject.Models;

namespace Eproject.ViewModel
{
    public class ProductsByCategoryVM
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }

        // Pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

       
    }
}
