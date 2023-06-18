﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hotel_management_api.Database;

#nullable disable

namespace hotel_management_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230618112332_v10_min_max_rating_value")]
    partial class v10_min_max_rating_value
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("hotel_management_api.Database.Model.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlock")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Returned")
                        .HasColumnType("bit");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastChange")
                        .HasColumnType("datetime2");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Comments");

                    b.HasCheckConstraint("CK_comment_rating_max_value", "Rating<=5 AND Rating>=1");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.District", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ProvineId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ProvineId");

                    b.ToTable("District");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Homelet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("DistrictId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Homelet");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoogleLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeletId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<int?>("HotelBenefitId")
                        .HasColumnType("int");

                    b.Property<int>("HotelCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("LogoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Star")
                        .HasColumnType("float");

                    b.Property<string>("USerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HomeletId");

                    b.HasIndex("HotelBenefitId")
                        .IsUnique()
                        .HasFilter("[HotelBenefitId] IS NOT NULL");

                    b.HasIndex("HotelCategoryId");

                    b.HasIndex("USerId");

                    b.ToTable("Hotel");

                    b.HasCheckConstraint("CK_hotel_start_max_value", "Star<=5 AND Star>=1");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.HotelBenefit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AirConditioner")
                        .HasColumnType("bit");

                    b.Property<bool?>("AllTimeFrontDesk")
                        .HasColumnType("bit");

                    b.Property<bool>("AllowPet")
                        .HasColumnType("bit");

                    b.Property<bool>("CarBorow")
                        .HasColumnType("bit");

                    b.Property<bool?>("Elevator")
                        .HasColumnType("bit");

                    b.Property<bool>("FreeBreakfast")
                        .HasColumnType("bit");

                    b.Property<bool>("Parking")
                        .HasColumnType("bit");

                    b.Property<bool>("Pool")
                        .HasColumnType("bit");

                    b.Property<bool?>("Resttaurant")
                        .HasColumnType("bit");

                    b.Property<bool>("WifiFree")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("HotelBenefit");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.HotelCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HotelCategory");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Provine", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Provine");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumOfBed")
                        .HasColumnType("int");

                    b.Property<int>("NumOfPeope")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("Refund")
                        .HasColumnType("bit");

                    b.Property<bool>("Reschedule")
                        .HasColumnType("bit");

                    b.Property<double?>("Square")
                        .HasColumnType("float");

                    b.Property<int>("TotalRoom")
                        .HasColumnType("int");

                    b.Property<string>("TypeOfBed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.RoomGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RooomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RooomId");

                    b.ToTable("RoomGallery");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Booking", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Room_Booking");

                    b.HasOne("hotel_management_api.Database.Model.AppUser", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_User_Booking");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Comment", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.Booking", "Booking")
                        .WithMany("Comments")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Booking_Comment");

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.District", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.Provine", "Provine")
                        .WithMany("Districts")
                        .HasForeignKey("ProvineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_Provine_District");

                    b.Navigation("Provine");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Homelet", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.District", "District")
                        .WithMany("Homelets")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_District_Homelet");

                    b.Navigation("District");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Hotel", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.Homelet", "Homelet")
                        .WithMany("Hotels")
                        .HasForeignKey("HomeletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_Homelet_Hotel");

                    b.HasOne("hotel_management_api.Database.Model.HotelBenefit", "HotelBenefit")
                        .WithOne("Hotel")
                        .HasForeignKey("hotel_management_api.Database.Model.Hotel", "HotelBenefitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_HotelBenefit_Hotel");

                    b.HasOne("hotel_management_api.Database.Model.HotelCategory", "HotelCategory")
                        .WithMany("Hotels")
                        .HasForeignKey("HotelCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_HotelCategory_Hotel");

                    b.HasOne("hotel_management_api.Database.Model.AppUser", "User")
                        .WithMany("Hotels")
                        .HasForeignKey("USerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Hotel");

                    b.Navigation("Homelet");

                    b.Navigation("HotelBenefit");

                    b.Navigation("HotelCategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Room", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Hotel_Room");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.RoomGallery", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.Room", "Room")
                        .WithMany("RoomGalleries")
                        .HasForeignKey("RooomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Room_RoomGallery");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hotel_management_api.Database.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("hotel_management_api.Database.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.AppUser", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Booking", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.District", b =>
                {
                    b.Navigation("Homelets");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Homelet", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.HotelBenefit", b =>
                {
                    b.Navigation("Hotel")
                        .IsRequired();
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.HotelCategory", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Provine", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("hotel_management_api.Database.Model.Room", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("RoomGalleries");
                });
#pragma warning restore 612, 618
        }
    }
}
