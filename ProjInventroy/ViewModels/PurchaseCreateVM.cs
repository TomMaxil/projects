using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjInventroy.ViewModels
{
    public class PurchaseCreateVM
    {
        public int SupplierId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public List<PurchaseItemVM> Items { get; set; }

        public List<SelectListItem>? Products { get; set; }

        public List<SelectListItem>? Suplliers { get; set; }
    }
}
