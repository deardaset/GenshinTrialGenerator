using GenshinTrialGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Core.Interfaces
{
    public interface IBossRepository
    {
        public Task CreateBossAsync(BossEntity boss);
        public Task<List<BossEntity>> GetAllBossesAsync();
        public Task<BossEntity?> GetBossAsync(Guid guid);
        public Task UpdateBossAsync(BossEntity boss);
        public Task DeleteBossAsync(BossEntity boss);
    }
}
