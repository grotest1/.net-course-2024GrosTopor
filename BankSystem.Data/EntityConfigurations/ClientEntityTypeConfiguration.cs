using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    internal class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");
            
            builder.Property(e => e.Id).HasColumnName("id");
            
            
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired().HasColumnName("first_name");
            builder.Property(c => c.Surname).HasMaxLength(100).IsRequired().HasColumnName("last_name");
            builder.Property(c => c.Age).IsRequired().HasColumnName("age");
            builder.Property(c => c.PersonalPhoneNumber).HasMaxLength(100).IsRequired().HasColumnName("phone");
            builder.Property(c => c.Passport).HasMaxLength(100).IsRequired().HasColumnName("passport");


            builder.HasKey(e => e.Id);
            //Остальные специфические свойстваClient
            //builder.Property(c => c.AccountNumber);
            //builder.Property(c => c.Balance);
            //builder.HasKey(e => e.Id);
            //builder.HasOne(cl => cl.Contract).WithOne(co =>
            //co.Counteragent).HasForeignKey<Client>(cl => cl.Id);
        }
    }
}

