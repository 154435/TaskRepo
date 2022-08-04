using Tasks.web;
using Tasks.web.Models;
using Microsoft.EntityFrameworkCore;
namespace Tasks.web.Data
{
    public class PracticeContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

       
        public PracticeContext(DbContextOptions<PracticeContext> options) : base(options)
        {

        }
        public DbSet<Tasks.web.Models.Category> Category { get; set; }
        
    }
}
