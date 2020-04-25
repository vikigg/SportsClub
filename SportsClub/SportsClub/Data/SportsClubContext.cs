using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsClub.Data.Models;

namespace SportsClub.Data
{
    public partial class SportsClubContext : DbContext
    {
        public SportsClubContext()
        {
        }

        public SportsClubContext(DbContextOptions<SportsClubContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = .\\SQLEXPRESS; Database= SportsClub; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__Player__TeamId__47DBAE45");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Team__SportId__3E52440B");

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.TrainerId)
                    .HasConstraintName("FK__Team__TrainerId__3F466844");
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
