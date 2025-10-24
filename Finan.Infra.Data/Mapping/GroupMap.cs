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
    public class GroupMap : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Description)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("text");

            builder.Property(prop => prop.Nature)
                .HasConversion(prop => prop, prop => prop)
                .IsRequired()
                .HasColumnName("Nature")
                .HasColumnType("smallint");

            builder.HasMany(a => a.Classifications)
                .WithOne(b => b.Group)
                .HasForeignKey("GroupId");
        }
    }
}
