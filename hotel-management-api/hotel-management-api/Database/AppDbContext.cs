using hotel_management_api.Database.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;

namespace hotel_management_api.Database
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {   
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<District> Districts { get; set; }  
        public DbSet<Homelet> Homelets { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelBenefit> HotelBenefits { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<Provine> Provines { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomGallery> RoomGalleries { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    tableName = tableName.Substring(6);
                    entityType.SetTableName(tableName);
                }
            }
            //config foreignkey
            builder.Entity<Provine>(opt =>
            {
                opt.HasMany(p => p.Districts)
                    .WithOne(d => d.Provine)
                    .HasForeignKey(d => d.ProvineId)
                    .HasConstraintName("FK_Provine_District")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<District>(opt =>
            {
                opt.HasMany(d => d.Homelets)
                    .WithOne(h => h.District)
                    .HasForeignKey(h => h.DistrictId)
                    .HasConstraintName("FK_District_Homelet")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Homelet>(opt =>
            {
                opt.HasMany(h => h.Hotels)
                    .WithOne(ht => ht.Homelet)
                    .HasForeignKey(ht => ht.HomeletId)
                    .HasConstraintName("FK_Homelet_Hotel")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<HotelCategory>(opt => 
            {
                opt.HasMany(hc => hc.Hotels)
                    .WithOne(h => h.HotelCategory)
                    .HasForeignKey(h => h.HotelCategoryId)
                    .HasConstraintName("FK_HotelCategory_Hotel")
                    .OnDelete (DeleteBehavior.Restrict);
            });
            builder.Entity<HotelBenefit>(opt =>
            {
                opt.HasOne(hb => hb.Hotel)
                    .WithOne(h => h.HotelBenefit)
                    .HasForeignKey<Hotel>(h => h.HotelBenefitId)
                    .HasConstraintName("FK_HotelBenefit_Hotel")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<AppUser>(opt =>
            {
                opt.HasIndex(u => u.Email).IsUnique(true);
                opt.HasMany(u => u.Hotels)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.USerId)
                .HasConstraintName("FK_User_Hotel")
                .IsRequired();
                opt.HasMany(u => u.Bookings)
                    .WithOne(b => b.User)
                    .HasForeignKey(b => b.UserId)
                    .HasConstraintName("FK_User_Booking")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Booking>(opt =>
            {
                opt.HasMany(b => b.Comments)
                    .WithOne(b => b.Booking)
                    .HasForeignKey(c => c.BookingId)
                    .HasConstraintName("FK_Booking_Comment")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Room>(opt => 
            {
                opt.HasMany(r => r.RoomGalleries)
                    .WithOne(rg => rg.Room)
                    .HasForeignKey(rg => rg.RooomId)
                    .HasConstraintName("FK_Room_RoomGallery")
                    .OnDelete(DeleteBehavior.Restrict);
                opt.HasMany(r => r.Bookings)
                    .WithOne(b => b.Room)
                    .HasForeignKey(b => b.RoomId)
                    .HasConstraintName("FK_Room_Booking")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Hotel>(opt =>
            {
                opt.HasMany(h => h.Rooms)
                    .WithOne(r => r.Hotel)
                    .HasForeignKey(r => r.HotelId)
                    .HasConstraintName("FK_Hotel_Room")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
