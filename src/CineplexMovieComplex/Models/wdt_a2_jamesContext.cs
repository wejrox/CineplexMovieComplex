using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CineplexMovieComplex.Models
{
    public partial class wdt_a2_jamesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=wdt_a2_james;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LastChange).HasColumnType("datetime");
            });

            modelBuilder.Entity<Cineplex>(entity =>
            {
                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.Location).IsRequired();

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.ShortDescription).IsRequired();
            });

            modelBuilder.Entity<CineplexMovie>(entity =>
            {
                entity.Property(e => e.CineplexMovieId).HasColumnName("CineplexMovieID");

                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Cineplex)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.CineplexId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__CineplexM__Cinep__2B3F6F97");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__CineplexM__Movie__2C3393D0");
            });

            modelBuilder.Entity<Enquiry>(entity =>
            {
                entity.Property(e => e.EnquiryId).HasColumnName("EnquiryID");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FromName).IsRequired();

                entity.Property(e => e.Message).IsRequired();
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ShortDescription).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<MovieTicket>(entity =>
            {
                entity.HasKey(e => e.MovieTicketId)
                    .HasName("PK__MovieBoo__B7EE5F047D2B3094");

                entity.Property(e => e.MovieTicketId).HasColumnName("MovieTicketID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.SeatId).HasColumnName("SeatID");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.MovieTicket)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__MovieBook__CartI__4F7CD00D");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.MovieTicket)
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__MovieBook__SeatI__5070F446");
            });

            modelBuilder.Entity<MovieComingSoon>(entity =>
            {
                entity.Property(e => e.MovieComingSoonId).HasColumnName("MovieComingSoonID");

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.ShortDescription).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.SeatId).HasColumnName("SeatID");

                entity.Property(e => e.CineplexMovieId).HasColumnName("CineplexMovieID");

                entity.HasOne(d => d.CineplexMovie)
                    .WithMany(p => p.Seat)
                    .HasForeignKey(d => d.CineplexMovieId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Seat__CineplexMo__47DBAE45");
            });
        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Cineplex> Cineplex { get; set; }
        public virtual DbSet<CineplexMovie> CineplexMovie { get; set; }
        public virtual DbSet<Enquiry> Enquiry { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieTicket> MovieTicket { get; set; }
        public virtual DbSet<MovieComingSoon> MovieComingSoon { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
    }
}