using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RSVP_website_SSTU.Models;

namespace RSVP_website_SSTU
{
    public partial class InfotechRSVPContext : DbContext
    {
        public InfotechRSVPContext()
        {
        }

        public InfotechRSVPContext(DbContextOptions<InfotechRSVPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rsvp> Rsvps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=ASUS-ROG-G14\\SSTU2021;Initial Catalog=InfotechRSVP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rsvp>(entity =>
            {
                entity.ToTable("RSVP");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.WillAttend);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
