using Microsoft.EntityFrameworkCore;

namespace Eproject.Models
{
    public class eCommerceContext : DbContext
    {
        public eCommerceContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> tbl_products { get; set; }

        public DbSet<Customer> tbl_customer { get; set; }

        public DbSet<Category> tbl_category { get; set; }

        public DbSet<Brand> tbl_brand { get; set; }

        public DbSet<Cart> tbl_cart { get; set; }

        public DbSet<CartItem> tbl_cartitem { get; set; }

        public DbSet<Order> tbl_order { get; set; }

        public DbSet<OrderItem> tbl_orderitem { get; set; }

        public DbSet<ProductReview> tbl_productreview { get; set; }

        public DbSet<User> users { get; set; }
    }
}
