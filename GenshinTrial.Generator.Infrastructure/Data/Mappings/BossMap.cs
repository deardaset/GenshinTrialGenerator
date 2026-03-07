using GenshinTrialGenerator.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrial.Generator.Infrastructure.Data.Mappings
{
    public class BossMap : IEntityTypeConfiguration<BossEntity>
    {
        public void Configure(EntityTypeBuilder<BossEntity> builder)
        {
            builder.ToTable("Bosses").HasKey(b => b.Guid);

            builder.Property(b => b.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
