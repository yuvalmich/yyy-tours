using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using yyytours.Models;

namespace yyytours
{
    public partial class yyyWebProjContext : DbContext
    {
        public yyyWebProjContext()
        {
        }

        public yyyWebProjContext(DbContextOptions<yyyWebProjContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=yyydb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<yyytours.Models.User> User { get; set; }

        public DbSet<yyytours.Models.Place> Place { get; set; }


    }
}
