﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistrationSystem.Data;

#nullable disable

namespace RegistrationSystem.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220629111342_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RegistrationSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Verified")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("declined")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@admin.com",
                            PasswordHash = new byte[] { 127, 104, 194, 44, 195, 70, 188, 77, 114, 151, 123, 213, 222, 53, 218, 65, 195, 149, 253, 105, 100, 47, 250, 156, 42, 234, 88, 136, 138, 144, 164, 29, 205, 112, 205, 18, 119, 119, 224, 39, 150, 19, 93, 50, 156, 97, 87, 67, 160, 46, 221, 84, 6, 143, 149, 113, 156, 177, 57, 76, 212, 99, 150, 142 },
                            PasswordSalt = new byte[] { 97, 203, 17, 136, 229, 68, 29, 255, 218, 72, 232, 169, 8, 139, 209, 80, 111, 7, 148, 206, 184, 78, 20, 159, 207, 70, 159, 89, 74, 255, 108, 182, 85, 76, 226, 105, 152, 172, 143, 248, 16, 73, 92, 226, 236, 106, 191, 183, 26, 249, 88, 194, 171, 59, 162, 167, 111, 242, 79, 51, 162, 30, 247, 142, 159, 36, 174, 57, 41, 14, 104, 45, 220, 217, 188, 123, 231, 118, 172, 14, 62, 222, 53, 31, 185, 191, 232, 236, 230, 208, 182, 109, 212, 107, 179, 195, 1, 166, 233, 167, 87, 119, 101, 181, 152, 190, 184, 88, 126, 190, 67, 73, 21, 22, 221, 226, 103, 244, 126, 167, 67, 157, 174, 73, 32, 165, 150, 137 },
                            Verified = "Yes",
                            declined = "No"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
