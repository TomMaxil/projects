using ProjInventroy.Models;
using ProjInventroy.Services.Email;

namespace ProjInventroy.Services.Inventroy
{
    public class InventroyService : IInventroyService
    {
        private readonly eCommerceContext _context;
        private readonly IEmailService _emailservice;

        public InventroyService(eCommerceContext context, IEmailService emailservice)
        {
            this._context = context;
            this._emailservice = emailservice;
        }

        public async Task HandleLowStockAsync(int productId)
        {
            var product = await _context.tbl_product.FindAsync(productId);

            if(product == null)
            {
                return;
            }

            if(product.StockQty <= product.MinStockQty)
            {
                string subject = "⚠ Low Stock Alert";
                string body = $@"
                <h3>Low Stock Warning</h3>
                <p><b>Product:</b> {product.Name}</p>
                <p><b>Available Qty:</b> {product.StockQty}</p>
                <p>Please restock soon.</p>";

                await _emailservice.SendAsync(
                    "bilal.nadeem2626@gmail.com",
                    subject,
                    body
                    );
            }
        }
    }
}
