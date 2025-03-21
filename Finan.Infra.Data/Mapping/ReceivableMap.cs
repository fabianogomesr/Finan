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
    public class ReceivableMap : IEntityTypeConfiguration<Receivable>
    {
        public void Configure(EntityTypeBuilder<Receivable> builder)
        {
            builder.ToTable("Receivable");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Type)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Type")
                .HasColumnType("tinyint");

            builder.Property(prop => prop.Value)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Value")
                .HasColumnType("decimal(18,2)");

            builder.Property(prop => prop.Discount)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Discount")
                .HasColumnType("decimal(18,2)");

            builder.Property(prop => prop.TotalReceivable)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("TotalReceivable")
                .HasColumnType("decimal(18,2)");

            builder.Property(prop => prop.IssueDate)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("IssueDate")
                .HasColumnType("datetime");

            builder.Property(prop => prop.DueDate)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("DueDate")
                .HasColumnType("datetime");

            builder.Property(prop => prop.CashFlowDate)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("CashFlowDate")
                .HasColumnType("datetime");

            builder.Property(prop => prop.AccrualPeriodDate)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("AccrualPeriodDate")
                .HasColumnType("datetime");

            builder.Property(prop => prop.Observation)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Observation")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Status)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("tinyint");

            builder.HasOne(a => a.CostCenter)
               .WithMany(b => b.Receivables)
               .HasForeignKey("CostCenterId");

            builder.HasOne(a => a.FinancialGroup)
               .WithMany(b => b.Receivables)
               .HasForeignKey("FinancialGroupId");

            builder.HasOne(a => a.FinancialClassification)
               .WithMany(b => b.Receivables)
               .HasForeignKey("FinancialClassificationId");
        }
    }
}
