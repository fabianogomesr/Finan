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
    public class ContractMap : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contract");

            builder.HasKey(prop => prop.Id);

            builder.HasOne(a => a.SubscriptionPlan)
            .WithMany(b => b.Contracts)
            .HasForeignKey("SubscriptionPlanId");

            builder.Property(prop => prop.SubscriptionPlanId)
                .IsRequired()
                .HasColumnName("SubscriptionPlanId");

        }
    }
}
