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
    public class BankTransactionMap : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.ToTable("BankTransaction");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Type)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Type")
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
               .WithMany(b => b.BankTransactions)
               .HasForeignKey("CostCenterId");

            builder.Property(prop => prop.CostCenterId)
                .IsRequired()
                .HasColumnName("CostCenterId");

            builder.HasOne(a => a.Group)
               .WithMany(b => b.BankTransactions)
               .HasForeignKey("GroupId")
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(prop => prop.GroupId)
                .IsRequired()
                .HasColumnName("GroupId");

            builder.HasOne(a => a.Classification)
               .WithMany(b => b.BankTransactions)
               .HasForeignKey("ClassificationId")
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(prop => prop.ClassificationId)
                .IsRequired()
                .HasColumnName("ClassificationId");

            builder.Property(prop => prop.AccountInId)
                .IsRequired()
                .HasColumnName("AccountInId");

            builder.Property(prop => prop.AccountOutId)
                .IsRequired()
                .HasColumnName("AccountOutId");

            builder.Property(prop => prop.ContractId)
                .IsRequired()
                .HasColumnName("ContractId");
        }
    }
}
