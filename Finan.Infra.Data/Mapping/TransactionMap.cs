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
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Description)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("text");

            builder.Property(prop => prop.Type)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Type")
                .HasColumnType("smallint");

            builder.Property(prop => prop.Value)
                .HasPrecision(18, 2) // Configuração para aceitar valores decimais
                .IsRequired()
                .HasColumnName("Value")
                .HasColumnType("numeric(18,2)");

            builder.Property(prop => prop.Discount)
                .HasPrecision(18, 2) // Configuração para aceitar valores decimais
                .IsRequired()
                .HasColumnName("Discount")
                .HasColumnType("numeric(18,2)");

            builder.Property(prop => prop.LateFee)
                .HasPrecision(18, 2) // Configuração para aceitar valores decimais
                .IsRequired()
                .HasColumnName("LateFee")
                .HasColumnType("numeric(18,2)");

            builder.Property(prop => prop.TotalPaid)
                .HasPrecision(18, 2) // Configuração para aceitar valores decimais
                .IsRequired()
                .HasColumnName("TotalPaid")
                .HasColumnType("numeric(18,2)");

            builder.Property(prop => prop.IssueDate)
                .IsRequired()
                .HasColumnName("IssueDate")
                .HasColumnType("timestamp");

            builder.Property(prop => prop.DueDate)
                .IsRequired()
                .HasColumnName("DueDate")
                .HasColumnType("timestamp");

            builder.Property(prop => prop.CashFlowDate)
                .IsRequired()
                .HasColumnName("CashFlowDate")
                .HasColumnType("timestamp");

            builder.Property(prop => prop.AccrualPeriodDate)
                .IsRequired()
                .HasColumnName("AccrualPeriodDate")
                .HasColumnType("timestamp");

            builder.Property(prop => prop.Observation)
                .IsRequired()
                .HasColumnName("Observation")
                .HasColumnType("text");

            builder.Property(prop => prop.Status)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("smallint");

            builder.HasOne(a => a.CostCenter)
               .WithMany(b => b.Transactions)
               .HasForeignKey("CostCenterId");

            builder.Property(prop => prop.CostCenterId)
                .IsRequired()
                .HasColumnName("CostCenterId");

            builder.HasOne(a => a.Group)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(prop => prop.GroupId)
                .IsRequired()
                .HasColumnName("GroupId");

            builder.HasOne(a => a.Classification)
               .WithMany(b => b.Transactions)
               .HasForeignKey(t => t.ClassificationId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(prop => prop.ClassificationId)
                .IsRequired()
                .HasColumnName("ClassificationId");

            builder.HasOne(a => a.Currency)
               .WithMany(b => b.Transactions)
               .HasForeignKey("CurrencyId");

            builder.Property(prop => prop.CurrencyId)
                .IsRequired()
                .HasColumnName("CurrencyId");
        }
    }
}
