using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Infrastructure.DataAccess
{
    public class GroceriesContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
        public GroceriesContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}