using GenshinTrialGenerator.Application.DTOs.Boss;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Interfaces.BossServices
{
    public interface IGetAllBossesService
    {
        public Task<List<BossModel>> RunAsync();
    }
}
