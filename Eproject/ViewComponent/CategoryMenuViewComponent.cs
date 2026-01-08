using Eproject.Models;
using Microsoft.AspNetCore.Mvc;

    public class CategoryMenuViewComponent : ViewComponent
    {
    private readonly eCommerceContext _context;

    public CategoryMenuViewComponent(eCommerceContext context)
    {
        this._context = context;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _context.tbl_category.ToList();
        return View(categories);
    }
}

