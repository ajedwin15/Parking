using DB.Models;
using Microsoft.EntityFrameworkCore;
using src.Parking.Api.DB.Models;

namespace DB
{
    public class ParkingContex : DbContext
    {
        public ParkingContex(DbContextOptions<ParkingContex> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
         }   

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<OfficialVehicle> OfficialVehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .ToTable("Vehicle");                

            modelBuilder.Entity<Resident>()
                .ToTable("Resident");     

            modelBuilder.Entity<OfficialVehicle>()
                .ToTable("OfficialVehicle"); 
        }
    }
}