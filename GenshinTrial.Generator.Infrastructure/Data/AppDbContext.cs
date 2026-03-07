using GenshinTrial.Generator.Infrastructure.Data.Mappings;
using GenshinTrialGenerator.Core.Entities;
using GenshinTrialGenerator.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrial.Generator.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<HeroEntity> Heroes { get; set; }
        public DbSet<BossEntity> Bosses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new HeroMap());
            modelBuilder.ApplyConfiguration(new BossMap());
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .Properties<Enum>()
                .HaveConversion<string>();
        }
    }
}
