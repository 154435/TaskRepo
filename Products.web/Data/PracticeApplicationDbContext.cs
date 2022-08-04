using Microsoft.EntityFrameworkCore;
using Products.web.Models;
namespace Products.web.Data
{
    public class PracticeApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public PracticeApplicationDbContext(DbContextOptions<PracticeApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<Customer> customers { get; set; }  
    }
}