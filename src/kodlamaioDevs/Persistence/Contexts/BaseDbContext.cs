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
        public DbSet<Technology> technologies { get; set; }
        public BaseDbContext(IConfiguration configuration, DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            _configuration = configuration;
        }

        public BaseDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               base.OnConfiguring(
                    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("KodlamaioDevsProjectConnectionString"))); 
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguage").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technology").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasOne(p => p.ProgrammingLanguage);
            });

            //Test veri oluşturma
            ProgrammingLanguage[] programmingLanguagesEntitySeeds =
            {
                new(1,"C#"),
                new(2,"Java"),
                new(3,"Python")
            };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguagesEntitySeeds);

            Technology[] technologiesEntitySeeds = { new(1,1, "WPF"),new(2,1,"ASP.NET"),new(3,2,"Spring") };
            modelBuilder.Entity<Technology>().HasData(technologiesEntitySeeds);
        }

    }
}
