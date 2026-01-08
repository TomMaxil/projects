using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjInventroy.ViewModels
{
    public class SaleCreateVM
    {
        public DateTime SaleDate { get; set; }

        public List<SaleItemVM> Items { get; set; }

        public List<SelectListItem>? Products { get; set; }
    }
}
