﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SportsStore.DAL.Contexts;
using System;

namespace SportsStore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SportsStore.Models.Cart.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int?>("ProductID");

                    b.Property<int>("Quantity");

                    b.HasKey("CartItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductID");

                    b.ToTable("Items","Sales");
                });

            modelBuilder.Entity("SportsStore.Models.CustomerModels.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApartmentNumber")
                        .HasMaxLength(6);

                    b.Property<string>("BuildingNumber")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<int>("CustomerId");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("AddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Addresses","Customer");
                });

            modelBuilder.Entity("SportsStore.Models.CustomerModels.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(9);

                    b.HasKey("CustomerId");

                    b.ToTable("Customers","Customer");
                });

            modelBuilder.Entity("SportsStore.Models.OrderModels.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<int>("CustomerId");

                    b.Property<bool>("GiftWrap");

                    b.Property<string>("OrderNumber");

                    b.Property<bool>("Shipped");

                    b.HasKey("OrderId");

                    b.HasIndex("AddressId");

                    b.ToTable("SalesOrders","Sales");
                });

            modelBuilder.Entity("SportsStore.Models.ProductModels.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.HasKey("ProductID");

                    b.ToTable("Products","Store");
                });

            modelBuilder.Entity("SportsStore.Models.Cart.CartItem", b =>
                {
                    b.HasOne("SportsStore.Models.OrderModels.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportsStore.Models.ProductModels.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("SportsStore.Models.CustomerModels.Address", b =>
                {
                    b.HasOne("SportsStore.Models.CustomerModels.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportsStore.Models.OrderModels.Order", b =>
                {
                    b.HasOne("SportsStore.Models.CustomerModels.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
