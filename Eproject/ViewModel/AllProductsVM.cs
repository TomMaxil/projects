using Eproject.Models;

namespace Eproject.ViewModel
{
    public class AllProductsVM

    {
        public List<Product> Products { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
