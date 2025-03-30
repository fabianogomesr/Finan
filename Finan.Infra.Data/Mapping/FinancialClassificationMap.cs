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
    public class FinancialClassificationMap : IEntityTypeConfiguration<FinancialClassification>
    {
        public void Configure(EntityTypeBuilder<FinancialClassification> builder)
        {
            builder.ToTable("FinancialClassification");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Description)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Type)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Type")
                .HasColumnType("tinyint");

            builder.Property(prop => prop.FinancialGroupId)
                .IsRequired()
                .HasColumnName("FinancialGroupId");

            builder.HasOne(a => a.FinancialGroup)
               .WithMany(b => b.FinancialClassifications)
               .HasForeignKey("FinancialGroupId");
        }
    }
}
