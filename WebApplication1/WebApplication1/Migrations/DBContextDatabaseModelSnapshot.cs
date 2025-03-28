﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(DBContextDatabase))]
    partial class DBContextDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("WebApplication1.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActivityPattern")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DietaryClass")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EnclosureId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan?>("FeedingTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Prey")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SecurityRequirement")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Size")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpaceRequirement")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ZooId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EnclosureId");

                    b.HasIndex("ZooId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("WebApplication1.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebApplication1.Models.Enclosure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Climate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HabitatType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SecurityLevel")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Size")
                        .HasColumnType("REAL");

                    b.Property<double?>("SpaceLeft")
                        .HasColumnType("REAL");

                    b.Property<int?>("ZooId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ZooId");

                    b.ToTable("Enclosures");
                });

            modelBuilder.Entity("WebApplication1.Models.Zoo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Zoos");
                });

            modelBuilder.Entity("WebApplication1.Models.Animal", b =>
                {
                    b.HasOne("WebApplication1.Models.Category", "Category")
                        .WithMany("Animals")
                        .HasForeignKey("CategoryId");

                    b.HasOne("WebApplication1.Models.Enclosure", "Enclosure")
                        .WithMany("Animals")
                        .HasForeignKey("EnclosureId");

                    b.HasOne("WebApplication1.Models.Zoo", null)
                        .WithMany("Animals")
                        .HasForeignKey("ZooId");

                    b.Navigation("Category");

                    b.Navigation("Enclosure");
                });

            modelBuilder.Entity("WebApplication1.Models.Enclosure", b =>
                {
                    b.HasOne("WebApplication1.Models.Zoo", null)
                        .WithMany("Enclosures")
                        .HasForeignKey("ZooId");
                });

            modelBuilder.Entity("WebApplication1.Models.Category", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("WebApplication1.Models.Enclosure", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("WebApplication1.Models.Zoo", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("Enclosures");
                });
#pragma warning restore 612, 618
        }
    }
}
