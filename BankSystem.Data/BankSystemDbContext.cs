using AutoMapper.Execution;
using BankSystem.Data.EntityConfigurations;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankSystem.Data
{
    public class BankSystemDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Currency> Currencies => Set<Currency>();

        //public BankSystemDbContext()
        //{

        //}
        //public BankSystemDbContext(IConfiguration configuration)
        //{
        //    var c = 1;
        //}


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseNpgsql("Host=localhost; Port = 5432; Database = postgres; Username = pg2; Password = pg2");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CurrancyEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
