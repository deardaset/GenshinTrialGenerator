using GenshinTrialGenerator.Application.DTOs.Boss;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Interfaces.BossServices
{
    public interface IUpdateBossService
    {
        public Task<BossModel> RunAsync(Guid guid, UpdateBossRequest request);
    }
}
