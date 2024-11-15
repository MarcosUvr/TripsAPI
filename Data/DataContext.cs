using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TripsAPI.Models.Domain;

namespace TripsAPI.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Origin)
                .WithMany()
                .HasForeignKey(t => t.IdOrigin)
                .OnDelete(DeleteBehavior.Restrict); // Esto previene la cascada

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Destination)
                .WithMany()
                .HasForeignKey(t => t.IdDestination)
                .OnDelete(DeleteBehavior.Restrict); // Esto previene la cascada

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Operator)
                .WithMany()
                .HasForeignKey(t => t.IdOperator)
                .OnDelete(DeleteBehavior.Cascade); // Si necesitas cascada aquí, déjalo

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.IdStatus)
                .OnDelete(DeleteBehavior.Cascade); // Si necesitas cascada aquí, déjalo

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<TripStatus> TripStatuses { get; set; }

    }
}
