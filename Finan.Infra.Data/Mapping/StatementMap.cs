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
                .HasColumnType("decimal(18,2)");

            builder.Property(prop => prop.Balance)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Balance")
                .HasColumnType("decimal(18,2)");

            builder.Property(prop => prop.FlowDate)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("FlowDate")
                .HasColumnType("datetime");

            builder.Property(prop => prop.ReconciledDate)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("ReconciledDate")
                .HasColumnType("datetime");

            builder.Property(prop => prop.Reversed)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Reversed")
                .HasColumnType("bit");

            builder.HasOne(a => a.Payment)
               .WithMany(b => b.Statements)
               .HasForeignKey("PaymentId");

            builder.HasOne(a => a.Receivable)
               .WithMany(b => b.Statements)
               .HasForeignKey("ReceivableId");

            builder.HasOne(a => a.Account)
               .WithMany(b => b.Statements)
               .HasForeignKey("AccountId");

            builder.HasOne(a => a.AccountDeposit)
               .WithMany(b => b.Statements)
               .HasForeignKey("AccountDepositId");
        }
    }
}
