using Finan.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("text");

            builder.Property(prop => prop.Agency)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Agency")
                .HasColumnType("text");

            builder.Property(prop => prop.Number)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Number")
                .HasColumnType("text");

            builder.Property(prop => prop.CreditLimit)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("CreditLimit")
                .HasColumnType("numeric(18,2)");

            builder.Property(prop => prop.Balance)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Balance")
                .HasColumnType("numeric(18,2)");

            builder.HasOne(a => a.Bank)
               .WithMany(b => b.Accounts)
               .HasForeignKey("BankId");

            builder.Property(prop => prop.BankId)
                .IsRequired()
                .HasColumnName("BankId");
        }
    }
}
