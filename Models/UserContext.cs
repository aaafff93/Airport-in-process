namespace Airport
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UserContext : DbContext
    {
        public UserContext()
            : base("name=UserContext")
        {
        }

        public virtual DbSet<Aircraft> Aircrafts { get; set; }
        public virtual DbSet<BoardingPass> BoardingPasses { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>()
                .HasMany(e => e.Flights)
                .WithRequired(e => e.Aircraft)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                .Property(e => e.BoardingTime)
                .HasPrecision(0);

            modelBuilder.Entity<Flight>()
                .Property(e => e.LastCallTime)
                .HasPrecision(0);

            modelBuilder.Entity<Flight>()
                .Property(e => e.OutTime)
                .HasPrecision(0);

            modelBuilder.Entity<Flight>()
                .Property(e => e.ArrivalTime)
                .HasPrecision(0);

            modelBuilder.Entity<Flight>()
                .HasMany(e => e.BoardingPasses)
                .WithRequired(e => e.Flight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                .HasOptional(e => e.Price)
                .WithRequired(e => e.Flight)
                .WillCascadeOnDelete();
        }
    }
}
