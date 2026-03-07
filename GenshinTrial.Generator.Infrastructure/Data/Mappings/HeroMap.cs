using GenshinTrialGenerator.Core.Entities;
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

            //builder.Property(h => h.Description)
            //    .HasColumnType("text");

            //builder.Property(h => h.Rarity)
            //    .HasConversion<string>();

            //builder.Property(h => h.Weapon)
            //    .HasConversion<string>();

            //builder.Property(h => h.Element)
            //    .HasConversion<string>();

            //builder.Property(h => h.Model)
            //    .HasConversion<string>();

            //builder.Property(h => h.TeamBonus)
            //    .HasConversion<string>();

            //builder.Property(h => h.Role)
            //    .HasConversion<string>();
        }
    }
}
