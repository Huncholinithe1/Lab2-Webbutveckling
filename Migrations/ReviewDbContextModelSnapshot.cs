﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReviewApi.Data;

#nullable disable

namespace ReviewApi.Migrations
{
    [DbContext(typeof(ReviewDbContext))]
    partial class ReviewDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReviewApi.Models.Recension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Betyg")
                        .HasColumnType("int");

                    b.Property<string>("Forfattare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Innehall")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProduktNamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SkapadDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recensioner");
                });
#pragma warning restore 612, 618
        }
    }
}
