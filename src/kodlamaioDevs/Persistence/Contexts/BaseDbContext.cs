using Core.Security.Entities;
using Core.Security.Enums;
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
        public DbSet<User> users { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<UserOperationClaim> userOperationClaims { get; set; }
        public DbSet<OperationClaim> operationClaims { get; set; }
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
            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("User").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p=>p.LastName).HasColumnName("LastName");
                a.Property(P=>P.Email).HasColumnName("Email");
                a.Property(p=>p.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(p=>p.PasswordHash).HasColumnName("PasswordHash");
                a.Property(p=>p.Status).HasColumnName("Status");
                a.Property(p=>p.AuthenticatorType).HasColumnName("AuthenticatorType");
                a.HasMany(p => p.UserOperationClaims);
                a.HasMany(p => p.RefreshTokens);
            });
            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaim").HasKey(k=>k.Id);
                a.Property(p=>p.Id).HasColumnName("Id");
                a.Property(p=>p.Name).HasColumnName("Name");
            });
            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaim").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p=>p.OperationClaimId).HasColumnName("OperationClaimId");
                a.HasOne(p => p.OperationClaim);
                a.HasOne(p=>p.User);
            });
            modelBuilder.Entity<RefreshToken>(a =>
            {
                a.ToTable("RefreshToken").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.Token).HasColumnName("Token");
                a.Property(p => p.Expires).HasColumnName("Expires");
                a.Property(p=>p.Created).HasColumnName("Created");
                a.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
                a.Property(p => p.Revoked).HasColumnName("Revoked");
                a.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
                a.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
                a.Property(p => p.ReasonRevoked).HasColumnName("ReasonRevoked");
                a.HasOne(p=>p.User);
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
