using Microsoft.EntityFrameworkCore;

namespace ProjInventroy.Models
{
    public class eCommerceContext : DbContext
    {
        public eCommerceContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> tbl_category { get; set; }

        public DbSet<Product> tbl_product { get; set; }

        public DbSet<Supplier> tbl_supplies { get; set; }

        public DbSet<Purchase> tbl_puchase { get; set; }

        public DbSet<PurchaseItem> tbl_puchaseitem { get; set; }

        public DbSet<Sale> tbl_sale { get; set; }

        public DbSet<SaleItem> tbl_saleitem { get; set; }

    }
}
