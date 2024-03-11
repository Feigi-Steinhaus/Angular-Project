using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public partial class MyTripsContext : DbContext,IContext
{
    public MyTripsContext()
    {
    }

    public MyTripsContext(DbContextOptions<MyTripsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookingPlace> BookingPlaces { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripType> TripTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source= DESKTOP-E0FAPSB\\SQLEXPRESS;Initial Catalog=myTrips; Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingPlace>(entity =>
        {
            entity.HasKey(e => e.BookingCode).HasName("PK__booking___FF29040E3F1AC6D9");

            entity.ToTable("booking_places");

            entity.Property(e => e.BookingCode).HasColumnName("booking_code");
            entity.Property(e => e.BookingDate)
                .HasColumnType("date")
                .HasColumnName("booking_date");
            entity.Property(e => e.BookingTime).HasColumnName("booking_time");
            entity.Property(e => e.NumOfPlaces).HasColumnName("num_of_places");
            entity.Property(e => e.TripCode).HasColumnName("trip_code");
            entity.Property(e => e.UserCode).HasColumnName("user_code");

            entity.HasOne(d => d.TripCodeNavigation).WithMany(p => p.BookingPlaces)
                .HasForeignKey(d => d.TripCode)
                .HasConstraintName("FK__booking_p__trip___3F466844");

            entity.HasOne(d => d.UserCodeNavigation).WithMany(p => p.BookingPlaces)
                .HasForeignKey(d => d.UserCode)
                .HasConstraintName("FK__booking_p__user___3E52440B");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripCode).HasName("PK__trips__F6B4147DE909A48C");

            entity.ToTable("trips");

            entity.Property(e => e.TripCode).HasColumnName("trip_code");
            entity.Property(e => e.AvailablePlaces).HasColumnName("available_places");
            entity.Property(e => e.DepartureTime).HasColumnName("departure_time");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.TripDate)
                .HasColumnType("date")
                .HasColumnName("trip_date");
            entity.Property(e => e.TripDestination)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("trip_destination");
            entity.Property(e => e.TripDurationHours).HasColumnName("trip_duration_hours");
            entity.Property(e => e.TypeCode).HasColumnName("type_code");

            entity.HasOne(d => d.TypeCodeNavigation).WithMany(p => p.Trips)
                .HasForeignKey(d => d.TypeCode)
                .HasConstraintName("FK__trips__type_code__398D8EEE");
        });

        modelBuilder.Entity<TripType>(entity =>
        {
            entity.HasKey(e => e.TypeCode).HasName("PK__trip_typ__2CB4DBF406F0D005");

            entity.ToTable("trip_types");

            entity.Property(e => e.TypeCode).HasColumnName("type_code");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserCode).HasName("PK__users__EDC4140E994614CB");

            entity.ToTable("users");

            entity.Property(e => e.UserCode).HasColumnName("user_code");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Family)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("family");
            entity.Property(e => e.FirstAidCertificate).HasColumnName("first_aid_certificate");
            entity.Property(e => e.LoginPassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("login_password");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
