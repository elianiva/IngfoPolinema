﻿// <auto-generated />
using System;
using IngfoPolinema.Infrastructure.DataSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IngfoPolinema.Infrastructure.Migrations
{
    [DbContext(typeof(PingDbContext))]
    [Migration("20221210004118_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("IngfoPolinema.Infrastructure.Models.PingHistory", b =>
                {
                    b.Property<Guid>("PingHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Duration")
                        .HasColumnType("REAL");

                    b.Property<Guid>("PingTargetId")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusCode")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimeStamp")
                        .HasColumnType("INTEGER");

                    b.HasKey("PingHistoryId");

                    b.HasIndex("PingTargetId");

                    b.ToTable("PingHistories", (string)null);
                });

            modelBuilder.Entity("IngfoPolinema.Infrastructure.Models.PingTarget", b =>
                {
                    b.Property<Guid>("PingTargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Interval")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Timeout")
                        .HasColumnType("REAL");

                    b.HasKey("PingTargetId");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.ToTable("PingTargets", (string)null);

                    b.HasData(
                        new
                        {
                            PingTargetId = new Guid("7b592b6f-e5f0-4c0e-b668-271110ad9a80"),
                            Address = "https://www.polinema.ac.id/",
                            Description = "Website utama Politeknik Negeri Malang",
                            Interval = 10000.0,
                            Name = "Polinema Main Website",
                            Timeout = 60000.0
                        },
                        new
                        {
                            PingTargetId = new Guid("db3023f1-f310-4afe-82ce-5ce8ae5986e5"),
                            Address = "https://lmsslc.polinema.ac.id/",
                            Description = "Learning Management System Politeknik Negeri Malang",
                            Interval = 10000.0,
                            Name = "Polinema LMS",
                            Timeout = 60000.0
                        },
                        new
                        {
                            PingTargetId = new Guid("41ccae87-1eb3-48cc-8b02-9d21502b23fa"),
                            Address = "http://siakad.polinema.ac.id/beranda",
                            Description = "Sistem Informasi Akademik Politeknik Negeri Malang",
                            Interval = 10000.0,
                            Name = "SIAKAD Polinema",
                            Timeout = 60000.0
                        },
                        new
                        {
                            PingTargetId = new Guid("c26faa8e-d1a2-4540-9457-f2c7f9325786"),
                            Address = "http://jti.polinema.ac.id/",
                            Description = "Website Jurusan Teknologi Informasi Politeknik Negeri Malang",
                            Interval = 10000.0,
                            Name = "JTI Polinema Main Website",
                            Timeout = 60000.0
                        },
                        new
                        {
                            PingTargetId = new Guid("dc0e4367-c6f2-465f-8500-59038c7b5681"),
                            Address = "https://slc.polinema.ac.id/spada/",
                            Description = "Portal LMS Politeknik Negeri Malang",
                            Interval = 10000.0,
                            Name = "SLC Polinema",
                            Timeout = 60000.0
                        });
                });

            modelBuilder.Entity("IngfoPolinema.Infrastructure.Models.PingHistory", b =>
                {
                    b.HasOne("IngfoPolinema.Infrastructure.Models.PingTarget", null)
                        .WithMany("Histories")
                        .HasForeignKey("PingTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IngfoPolinema.Infrastructure.Models.PingTarget", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
