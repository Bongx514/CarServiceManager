using CarServiceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CarServiceManager.Data
{
    public class CarServiceContext : DbContext
    {
        public CarServiceContext(DbContextOptions<CarServiceContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<VehicleMakeLookUp> VehicleMakeLookUp { get; set; }
        public DbSet<MaintenanceTypeLookUp> MaintenanceTypeLookUp { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().HasKey(u => u.pkiUserID);
            modelBuilder.Entity<Vehicles>().HasKey(u => u.pkiVehicleID);
            modelBuilder.Entity<MaintenanceRecord>().HasKey(u => u.pkiMaintenanceID);
            modelBuilder.Entity<VehicleMakeLookUp>().HasNoKey();
            modelBuilder.Entity<MaintenanceTypeLookUp>().HasNoKey();
        }
    }
}
