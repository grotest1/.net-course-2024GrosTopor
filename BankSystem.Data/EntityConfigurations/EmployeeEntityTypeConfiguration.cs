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
    internal class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees");
            
            builder.Property(e => e.Id).HasColumnName("id");
            
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired().HasColumnName("first_name");
            builder.Property(c => c.Surname).HasMaxLength(100).IsRequired().HasColumnName("last_name");
            builder.Property(c => c.Age).IsRequired().HasColumnName("age");
            builder.Property(c => c.PersonalPhoneNumber).HasMaxLength(100).IsRequired().HasColumnName("phone");
            builder.Property(c => c.Passport).HasMaxLength(100).IsRequired().HasColumnName("passport");
            builder.Property(c => c.Birthday).IsRequired().HasColumnName("birthday");
            builder.Property(c => c.Contract).HasMaxLength(1000).IsRequired().HasColumnName("contract");
            builder.Property(c => c.Salary).IsRequired().HasColumnName("salary");

            builder.HasKey(e => e.Id);
        }
    }
}

