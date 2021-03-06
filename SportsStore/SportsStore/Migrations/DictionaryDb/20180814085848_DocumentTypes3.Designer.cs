﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsStore.DAL.Contexts;

namespace SportsStore.Migrations.DictionaryDb
{
    [DbContext(typeof(DictionaryDbContext))]
    [Migration("20180814085848_DocumentTypes3")]
    partial class DocumentTypes3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SportsStore.Models.DictionaryModels.DocumentType", b =>
                {
                    b.Property<int>("DocumentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("DocumentKind")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Symbol")
                        .IsRequired();

                    b.HasKey("DocumentTypeId");

                    b.ToTable("DocumentTypes","Dictionary");
                });
#pragma warning restore 612, 618
        }
    }
}
