using Eproject.Models;

namespace Eproject.ViewModel
{
    public class HomeVM
    {
        public List<Category> RandomCategories { get; set; }
        public Dictionary<string, List<Product>> CategoryProducts { get; set; }

        public List<Brand> RandomBrands { get; set; }
    }
}
