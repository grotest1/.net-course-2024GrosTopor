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
            builder.Property(e => e.CurrencyId).HasColumnName("currency_id");
            builder.Property(c => c.Amount).IsRequired().HasColumnName("amount");
            
            builder.HasKey(e => e.Id);

            builder.HasOne(cl => cl.Client).WithMany(co => co.Accounts).HasForeignKey(cl => cl.ClientId);
            builder.HasOne(cl => cl.Currency).WithMany(co => co.Accounts).HasForeignKey(cl => cl.CurrencyId);
        }
    }
}

