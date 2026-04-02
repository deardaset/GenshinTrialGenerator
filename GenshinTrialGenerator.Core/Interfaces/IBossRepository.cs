using GenshinTrialGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Core.Interfaces
{
    public interface IBossRepository
    {
        public Task CreateBossAsync(Boss boss);
        public Task<(List<Boss>, int total)> GetAllBossesAsync(int page, int pageSize, string? search, string? sort, string? element);
        public Task<Boss?> GetBossAsync(Guid guid);
        public Task UpdateBossAsync(Boss boss);
        public Task DeleteBossAsync(Boss boss);
    }
}
