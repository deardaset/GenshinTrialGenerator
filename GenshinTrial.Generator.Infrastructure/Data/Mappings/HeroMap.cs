using GenshinTrialGenerator.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrial.Generator.Infrastructure.Data.Mappings
{
    public class HeroMap : IEntityTypeConfiguration<HeroEntity>
    {
        public void Configure(EntityTypeBuilder<HeroEntity> builder)
        {
            builder.ToTable("Heroes").HasKey(h => h.Guid);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
