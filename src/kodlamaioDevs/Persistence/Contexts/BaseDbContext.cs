using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration _configuration { get; set; }
        public DbSet<ProgrammingLanguage> programmingLanguages { get; set; }

        public BaseDbContext(IConfiguration configuration, DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    base.OnConfiguring(
            //        optionsBuilder.UseSglServer(_configuration.GetConnectionString("SomeConnectionString")); 
            //}

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguage").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });


            //Test veri oluşturma
            ProgrammingLanguage[] programmingLanguagesEntitySeeds =
            {
                new(1,"C#"),
                new(2,"Java"),
                new(3,"Python")
            };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguagesEntitySeeds);
        }

    }
}
