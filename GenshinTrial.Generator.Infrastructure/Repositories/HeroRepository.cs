using GenshinTrial.Generator.Infrastructure.Data;
using GenshinTrialGenerator.Core.Models;
using GenshinTrialGenerator.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GenshinTrialGenerator.Infrastructure.Entities;

namespace GenshinTrialGenerator.Infrastructure.Repositories
{
    public class HeroRepository(AppDbContext context, IMapper mapper) : IHeroRepository
    {
        public async Task CreateHeroAsync(Hero hero)
        {
            context.Heroes.Add(mapper.Map<HeroEntity>(hero));
            await context.SaveChangesAsync();
        }

        public async Task DeleteHeroAsync(Hero hero)
        {
            context.Heroes.Remove(mapper.Map<HeroEntity>(hero));
            await context.SaveChangesAsync();
        }

        public async Task<List<Hero>> GetAllHeroesAsync()
        {
            return mapper.Map<List<Hero>>(await context.Heroes.AsNoTracking().ToListAsync());
        }

        public async Task<Hero?> GetHeroAsync(Guid guid)
        {
            return mapper.Map<Hero>(await context.Heroes.AsNoTracking().FirstOrDefaultAsync(h => h.Guid == guid));
        }

        public async Task UpdateHeroAsync(Hero hero)
        {
            context.Heroes.Update(mapper.Map<HeroEntity>(hero));
            await context.SaveChangesAsync();
        }
    }
}
