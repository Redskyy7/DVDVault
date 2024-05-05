﻿// <auto-generated />
using System;
using DVDVault.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DVDVault.Infra.Migrations
{
    [DbContext(typeof(DVDVaultContext))]
    partial class DVDVaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DVDVault.Domain.Models.DVD", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Available")
                        .HasColumnType("boolean");

                    b.Property<int>("Copies")
                        .HasColumnType("integer")
                        .HasColumnName("Copies");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("DeletedAt");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DirectorId")
                        .HasColumnType("integer");

                    b.Property<int>("Genre")
                        .HasColumnType("char(40)")
                        .HasColumnName("Genre");

                    b.Property<DateTime>("Published")
                        .HasColumnType("timestamp")
                        .HasColumnName("Published");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("char(75)")
                        .HasColumnName("Title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("DVDs", null, t =>
                        {
                            t.Property("DeletedAt")
                                .HasColumnName("DeletedAt1");
                        });
                });

            modelBuilder.Entity("DVDVault.Domain.Models.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("DeletedAt");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("char(20)")
                        .HasColumnName("Name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("char(100)")
                        .HasColumnName("Surname");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Directors", null, t =>
                        {
                            t.Property("DeletedAt")
                                .HasColumnName("DeletedAt1");
                        });
                });

            modelBuilder.Entity("DVDVault.Domain.Models.DVD", b =>
                {
                    b.HasOne("DVDVault.Domain.Models.Director", "Director")
                        .WithMany("DVDs")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("DVDVault.Domain.Models.Director", b =>
                {
                    b.Navigation("DVDs");
                });
#pragma warning restore 612, 618
        }
    }
}
