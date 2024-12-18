﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using T2HackathonCase2.Data;

#nullable disable

namespace T2HackathonCase2.Migrations
{
    [DbContext(typeof(WeekendWayDbContext))]
    [Migration("20241217173134_ChangedLocationsw")]
    partial class ChangedLocationsw
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("T2HackathonCase2.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("T2HackathonCase2.Entities.OpeningHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Recurence")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Start")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("OpeningHours");
                });

            modelBuilder.Entity("T2HackathonCase2.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<long?>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Longtute")
                        .HasColumnType("REAL");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("T2HackathonCase2.Entities.Location", b =>
                {
                    b.HasOne("T2HackathonCase2.Entities.User", null)
                        .WithMany("Locations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("T2HackathonCase2.Entities.OpeningHours", b =>
                {
                    b.HasOne("T2HackathonCase2.Entities.Location", "Location")
                        .WithMany("OpeningHours")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("T2HackathonCase2.Entities.Location", b =>
                {
                    b.Navigation("OpeningHours");
                });

            modelBuilder.Entity("T2HackathonCase2.Entities.User", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
