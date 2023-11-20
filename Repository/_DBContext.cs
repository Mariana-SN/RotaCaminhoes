using Microsoft.EntityFrameworkCore;
using TruckRouteAPI.Models;

namespace TruckRouteAPI.Repository
{
    public class _DBContext : DbContext
    {
        public _DBContext(DbContextOptions<_DBContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> users { get; set; }

        public DbSet<Models.Route> routers { get; set; }
    }
}
