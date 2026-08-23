using CarServiceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CarServiceManager.Data
{
    public class CarServiceContext : DbContext
    {
        public CarServiceContext(DbContextOptions<CarServiceContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().HasKey(u => u.pkiUserID);
        }
    }
}
