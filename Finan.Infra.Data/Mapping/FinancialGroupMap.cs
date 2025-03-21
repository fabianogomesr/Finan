using Finan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Mapping
{
    public class FinancialGroupMap : IEntityTypeConfiguration<FinancialGroup>
    {
        public void Configure(EntityTypeBuilder<FinancialGroup> builder)
        {
            builder.ToTable("FinancialGroup");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Description)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("varchar(100)");

            builder.HasMany(a => a.FinancialClassifications)
                .WithOne(b => b.FinancialGroup)
                .HasForeignKey("FinancialGroupId");
        }
    }
}
