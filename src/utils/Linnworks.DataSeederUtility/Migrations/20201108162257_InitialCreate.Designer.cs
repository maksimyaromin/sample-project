﻿// <auto-generated />
using System;
using Linnworks.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Linnworks.DataSeederUtility.Migrations
{
    [DbContext(typeof(LinnworksDbContext))]
    [Migration("20201108162257_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<int>("RegionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(512);

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OrderPriorityId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderedAt")
                        .HasColumnType("DATE");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrderPriorityId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.OrderPriority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnName("OrderPriority")
                        .HasColumnType("NCHAR(1)");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Symbol")
                        .IsUnique();

                    b.ToTable("OrderPriorities");
                });

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(512);

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SalesChannel")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<DateTime?>("ShippedAt")
                        .HasColumnType("DATE");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("DECIMAL(100,2)");

                    b.Property<decimal>("TotalProfit")
                        .HasColumnType("DECIMAL(100,2)");

                    b.Property<decimal>("TotalRevenue")
                        .HasColumnType("DECIMAL(100,2)");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("DECIMAL(100,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("DECIMAL(100,2)");

                    b.Property<int>("UnitsSold")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.Country", b =>
                {
                    b.HasOne("Linnworks.Core.Domain.Entities.Region", "Region")
                        .WithMany("Countries")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.Order", b =>
                {
                    b.HasOne("Linnworks.Core.Domain.Entities.OrderPriority", "OrderPriority")
                        .WithMany("Orders")
                        .HasForeignKey("OrderPriorityId");
                });

            modelBuilder.Entity("Linnworks.Core.Domain.Entities.Sale", b =>
                {
                    b.HasOne("Linnworks.Core.Domain.Entities.Country", "Country")
                        .WithMany("Sales")
                        .HasForeignKey("CountryId");

                    b.HasOne("Linnworks.Core.Domain.Entities.Item", "Item")
                        .WithMany("Sales")
                        .HasForeignKey("ItemId");

                    b.HasOne("Linnworks.Core.Domain.Entities.Order", "Order")
                        .WithOne("Sale")
                        .HasForeignKey("Linnworks.Core.Domain.Entities.Sale", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}