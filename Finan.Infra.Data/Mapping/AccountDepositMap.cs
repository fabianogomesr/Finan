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
    public class AccountDepositMap : IEntityTypeConfiguration<AccountDeposit>
    {
        public void Configure(EntityTypeBuilder<AccountDeposit> builder)
        {
            builder.ToTable("AccountDeposit");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Type)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("tinyint");

            builder.Property(prop => prop.Date)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("datetime");

            builder.Property(prop => prop.Value)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Value")
                .HasColumnType("decimal(18,2)");

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

            builder.HasOne(a => a.CostCenter)
               .WithMany(b => b.AccountDeposits)
               .HasForeignKey("CostCenterId");

            builder.HasOne(a => a.FinancialGroup)
               .WithMany(b => b.AccountDeposits)
               .HasForeignKey("FinancialGroupId");

            builder.HasOne(a => a.FinancialClassification)
               .WithMany(b => b.AccountDeposits)
               .HasForeignKey("FinancialClassificationId");
        }
    }
}
