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
    public class StatementMap : IEntityTypeConfiguration<Statement>
    {
        public void Configure(EntityTypeBuilder<Statement> builder)
        {
            builder.ToTable("Statement");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Value)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Value")
                .HasColumnType("numeric(18,2)");

            builder.Property(prop => prop.Balance)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Balance")
                .HasColumnType("numeric(18,2)");

            builder.Property(prop => prop.FlowDate)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("FlowDate")
                .HasColumnType("timestamp");

            builder.Property(prop => prop.ReconciledDate)
                .HasConversion(prop => prop, prop => prop)
                .HasColumnName("ReconciledDate")
                .HasColumnType("timestamp");

            builder.Property(prop => prop.Reversed)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Reversed")
                .HasColumnType("boolean");

            builder.HasOne(a => a.Transaction)
               .WithMany(b => b.Statements)
               .HasForeignKey("TransactionId");

            builder.Property(prop => prop.TransactionId)
                .HasColumnName("TransactionId");

            builder.HasOne(a => a.Account)
               .WithMany(b => b.Statements)
               .HasForeignKey("AccountId");

            builder.Property(prop => prop.AccountId)
                .IsRequired()
                .HasColumnName("AccountId");

            builder.HasOne(a => a.BankTransaction)
               .WithMany(b => b.Statements)
               .HasForeignKey("BankTransactionId");

            builder.Property(prop => prop.BankTransactionId)
                .HasColumnName("BankTransactionId");
        }
    }
}
