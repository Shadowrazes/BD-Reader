using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;

namespace BD_Reader.Models
{
    public partial class WRCContext : DbContext
    {
        public WRCContext()
        {
        }

        public WRCContext(DbContextOptions<WRCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Result> Results { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        private string DbPath = @"Assets\WRC.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directoryPath = Directory.GetCurrentDirectory();
                directoryPath = directoryPath.Remove(directoryPath.LastIndexOf("bin"));
                DbPath = directoryPath + DbPath;
                optionsBuilder.UseSqlite("Data source=" + DbPath);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.FullName);

                entity.Property(e => e.FullName).HasColumnName("Full Name");

                entity.Property(e => e.AvgFinish)
                    .HasColumnType("DOUBLE")
                    .HasColumnName("AVG Finish");

                entity.Property(e => e.CarId).HasColumnName("Car ID");

                entity.Property(e => e.TeamName).HasColumnName("Team Name");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.TeamNameNavigation)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.TeamName);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Date).HasColumnType("DATETIME");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => new { e.DriverFullName, e.StageName });

                entity.Property(e => e.DriverFullName).HasColumnName("Driver Full Name");

                entity.Property(e => e.StageName).HasColumnName("Stage Name");

                entity.Property(e => e.EventName).HasColumnName("Event Name");

                entity.Property(e => e.Time).HasColumnType("DATETIME");

                entity.HasOne(d => d.DriverFullNameNavigation)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.DriverFullName)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.EventNameNavigation)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.EventName);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Name);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
