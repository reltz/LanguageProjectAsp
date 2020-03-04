using System;
using LanguageProjectAsp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LanguageProjectAsp.DAL
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-NCFTL7L\\SQLEXPRESS;Initial Catalog=languageResearch;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.AgeGroup).IsUnicode(false);

                entity.Property(e => e.Coordinate).IsUnicode(false);

                entity.Property(e => e.Geo).IsUnicode(false);

                entity.Property(e => e.ScalarFactor).IsUnicode(false);

                entity.Property(e => e.Sex).IsUnicode(false);

                entity.Property(e => e.StudentResponse).IsUnicode(false);

                entity.Property(e => e.Uom).IsUnicode(false);

                entity.Property(e => e.Vector).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
