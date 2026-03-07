using GenshinTrialGenerator.Application.DTOs.Boss;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Interfaces.BossServices
{
    public interface ICreateBossService
    {
        public Task<BossModel> RunAsync(CreateBossRequest request);
    }
}
