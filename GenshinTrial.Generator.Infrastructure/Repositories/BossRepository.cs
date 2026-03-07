using GenshinTrial.Generator.Infrastructure.Data;
using GenshinTrialGenerator.Core.Entities;
using GenshinTrialGenerator.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Infrastructure.Repositories
{
    public class BossRepository(AppDbContext context) : IBossRepository
    {
        public async Task CreateBossAsync(BossEntity boss)
        {
            context.Bosses.Add(boss);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBossAsync(BossEntity boss)
        {
            context.Bosses.Remove(boss);
            await context.SaveChangesAsync();
        }

        public async Task<List<BossEntity>> GetAllBossesAsync()
        {
            return await context.Bosses.AsNoTracking().ToListAsync();
        }

        public async Task<BossEntity?> GetBossAsync(Guid guid)
        {
            return await context.Bosses.AsNoTracking().FirstOrDefaultAsync(b => b.Guid == guid);
        }

        public async Task UpdateBossAsync(BossEntity boss)
        {
            context.Bosses.Update(boss);
            await context.SaveChangesAsync();
        }
    }
}
