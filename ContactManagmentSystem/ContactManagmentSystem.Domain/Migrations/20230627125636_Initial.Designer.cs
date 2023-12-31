﻿// <auto-generated />
using System;
using ContactManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactManagementSystem.Domain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230627125636_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContactManagmentSystem.Domain.Models.BaseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BaseEntity");
                });

            modelBuilder.Entity("ContactManagmentSystem.Domain.Models.Contact", b =>
                {
                    b.HasBaseType("ContactManagmentSystem.Domain.Models.BaseEntity");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Contact");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9895b85-9304-42b7-a5d9-08db6cf9923a"),
                            CreatedDate = new DateTime(2023, 6, 27, 8, 56, 35, 854, DateTimeKind.Local).AddTicks(9842),
                            ModifiedDate = new DateTime(2023, 6, 27, 8, 56, 35, 854, DateTimeKind.Local).AddTicks(9913),
                            BirthDate = new DateTime(2001, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "HeshoOdeh@hesho.com",
                            FirstName = "Hesho",
                            LastName = "Odeh",
                            PhoneNumber = "123-456-7890"
                        });
                });

            modelBuilder.Entity("ContactManagmentSystem.Domain.Models.Contact", b =>
                {
                    b.HasOne("ContactManagmentSystem.Domain.Models.BaseEntity", null)
                        .WithOne()
                        .HasForeignKey("ContactManagmentSystem.Domain.Models.Contact", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
