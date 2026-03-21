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
    public class BossRepository(AppDbContext context, IMapper mapper) : IBossRepository
    {
        public async Task CreateBossAsync(Boss boss)
        {
            context.Bosses.Add(mapper.Map<BossEntity>(boss));
            await context.SaveChangesAsync();
        }

        public async Task DeleteBossAsync(Boss boss)
        {
            context.Bosses.Remove(mapper.Map<BossEntity>(boss));
            await context.SaveChangesAsync();
        }

        public async Task<List<Boss>> GetAllBossesAsync()
        {
            return mapper.Map<List<Boss>>(await context.Bosses.AsNoTracking().ToListAsync());
        }

        public async Task<Boss?> GetBossAsync(Guid guid)
        {
            return mapper.Map<Boss>(await context.Bosses.AsNoTracking().FirstOrDefaultAsync(b => b.Guid == guid));
        }

        public async Task UpdateBossAsync(Boss boss)
        {
            context.Bosses.Update(mapper.Map<BossEntity>(boss));
            await context.SaveChangesAsync();
        }
    }
}
