using GenshinTrial.Generator.Infrastructure.Data;
using GenshinTrialGenerator.Core.Entities;
using GenshinTrialGenerator.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Infrastructure.Repositories
{
    public class HeroRepository(AppDbContext context) : IHeroRepository
    {
        public async Task CreateHeroAsync(HeroEntity hero)
        {
            context.Heroes.Add(hero);
            await context.SaveChangesAsync();
        }

        public async Task DeleteHeroAsync(HeroEntity hero)
        {
            context.Heroes.Remove(hero);
            await context.SaveChangesAsync();
        }

        public async Task<List<HeroEntity>> GetAllHeroesAsync()
        {
            return await context.Heroes.AsNoTracking().ToListAsync();
        }

        public async Task<HeroEntity?> GetHeroAsync(Guid guid)
        {
            return await context.Heroes.AsNoTracking().FirstOrDefaultAsync(h => h.Guid == guid);
        }

        public async Task UpdateHeroAsync(HeroEntity hero)
        {
            context.Heroes.Update(hero);
            await context.SaveChangesAsync();
        }
    }
}
