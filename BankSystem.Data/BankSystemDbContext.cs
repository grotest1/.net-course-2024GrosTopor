using BankSystem.Data.EntityConfigurations;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Data
{
    internal class BankSystemDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Port = 5432; Database = postgres@localhost; Username = postgres; Password = sasihyica");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new
                ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
