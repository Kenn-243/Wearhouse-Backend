using Microsoft.EntityFrameworkCore;
using Wearhouse.Models;

namespace Wearhouse.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Item> Item { get; set; }
    }
}
