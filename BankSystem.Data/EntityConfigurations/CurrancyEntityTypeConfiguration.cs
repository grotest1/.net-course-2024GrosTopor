using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    internal class CurrancyEntityTypeConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("currancies");
            
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired().HasColumnName("title");
            builder.Property(c => c.Code).IsRequired().HasColumnName("code");

            builder.HasKey(e => e.Id);
        }
    }
}

