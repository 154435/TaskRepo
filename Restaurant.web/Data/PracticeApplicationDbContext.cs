using Microsoft.EntityFrameworkCore;
using Restaurant.web.Model;
using Restaurant.web.Models;

namespace Restaurant.web.Data
{
    public class PracticeApplicationDbContext : DbContext
    {
        public DbSet<Foodmenu> Foodmenu { get; set; }

        public DbSet<Customers> Customers { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<FoodCategory> FoodCategory { get; set; }



        public PracticeApplicationDbContext(DbContextOptions<PracticeApplicationDbContext> options) : base(options)
        {

        }
    }
}
