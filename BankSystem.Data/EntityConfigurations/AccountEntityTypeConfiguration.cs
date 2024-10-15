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
    internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts");
            
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ClientId).HasColumnName("client_id");
            builder.Property(c => c.Amount).IsRequired().HasColumnName("amount");
            builder.Property(c => c.CurrencyName).HasMaxLength(100).IsRequired().HasColumnName("currency_name");
            //builder.Property(c => c.Currency.Name).HasMaxLength(100).IsRequired().HasColumnName("currency_name");
            
            builder.HasKey(e => e.Id);

            builder.HasOne(cl => cl.Client).WithMany(co => co.Accounts).HasForeignKey(cl => cl.ClientId);
        }
    }
}

