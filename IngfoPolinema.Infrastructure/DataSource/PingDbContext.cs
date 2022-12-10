﻿using IngfoPolinema.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IngfoPolinema.Infrastructure.DataSource;

public class PingDbContext : DbContext
{
    public DbSet<PingTarget> PingTargets { get; set; }

    public PingDbContext(DbContextOptions<PingDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // configure constraints and table names
        builder.Entity<PingTarget>()
               .ToTable("PingTargets")
               .HasIndex(target => target.Address)
               .IsUnique();
        builder.Entity<PingHistory>()
               .ToTable("PingHistories");

        // configure autogenerated id
        builder.Entity<PingTarget>()
               .Property(target => target.PingTargetId)
               .ValueGeneratedOnAdd();
        builder.Entity<PingHistory>()
               .Property(target => target.PingHistoryId)
               .ValueGeneratedOnAdd();

        // configure field conversion
        builder.Entity<PingTarget>()
               .Property(target => target.Address)
               .HasConversion(address => address.AbsoluteUri, address => new Uri(address));
        builder.Entity<PingTarget>()
               .Property(target => target.Interval)
               .HasConversion(interval => interval.TotalMilliseconds, interval => TimeSpan.FromMilliseconds(interval));
        builder.Entity<PingTarget>()
               .Property(target => target.Timeout)
               .HasConversion(timeout => timeout.TotalMilliseconds, timeout => TimeSpan.FromMilliseconds(timeout));
        builder.Entity<PingHistory>()
               .Property(target => target.Duration)
               .HasConversion(duration => duration.TotalMilliseconds, duration => TimeSpan.FromMilliseconds(duration));
        builder.Entity<PingHistory>()
               .Property(history => history.TimeStamp)
               .HasConversion(timestamp => timestamp.ToUnixTimeMilliseconds(), timestamp => DateTimeOffset.FromUnixTimeMilliseconds(timestamp));
        builder.Entity<PingHistory>()
               .Property(history => history.StatusCode)
               .HasConversion(statusCode => (int)statusCode, statusCode => (HttpStatusCode)Enum.ToObject(typeof(HttpStatusCode), statusCode));

        // seed some initial data
        builder.Entity<PingTarget>().HasData(new[]
        {
            new PingTarget
            {
                PingTargetId = Guid.NewGuid(),
                Name = "Polinema Main Website",
                Address = new Uri("https://www.polinema.ac.id/"),
                Description = "Website utama Politeknik Negeri Malang",
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromMinutes(1),
            },
            new PingTarget
            {
                PingTargetId = Guid.NewGuid(),
                Name = "Polinema LMS",
                Address = new Uri("https://lmsslc.polinema.ac.id/"),
                Description = "Learning Management System Politeknik Negeri Malang",
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromMinutes(1),
            },
            new PingTarget
            {
                PingTargetId = Guid.NewGuid(),
                Name = "SIAKAD Polinema",
                Address = new Uri("http://siakad.polinema.ac.id/beranda"),
                Description = "Sistem Informasi Akademik Politeknik Negeri Malang",
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromMinutes(1),
            },
            new PingTarget
            {
                PingTargetId = Guid.NewGuid(),
                Name = "JTI Polinema Main Website",
                Address = new Uri("http://jti.polinema.ac.id/"),
                Description = "Website Jurusan Teknologi Informasi Politeknik Negeri Malang",
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromMinutes(1),
            },
            new PingTarget
            {
                PingTargetId = Guid.NewGuid(),
                Name = "SLC Polinema",
                Address = new Uri("https://slc.polinema.ac.id/spada/"),
                Description = "Portal LMS Politeknik Negeri Malang",
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromMinutes(1),
            }
        });
    }
}
