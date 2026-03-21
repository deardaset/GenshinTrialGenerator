using GenshinTrialGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Core.Interfaces
{
    public interface IBossRepository
    {
        public Task CreateBossAsync(Boss boss);
        public Task<List<Boss>> GetAllBossesAsync();
        public Task<Boss?> GetBossAsync(Guid guid);
        public Task UpdateBossAsync(Boss boss);
        public Task DeleteBossAsync(Boss boss);
    }
}
