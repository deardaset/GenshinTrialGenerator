using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Boss;
using GenshinTrialGenerator.Application.Interfaces.BossServices;
using GenshinTrialGenerator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.BossServices
{
    public class GetAllBossesService(IBossRepository repository, IMapper mapper) : IGetAllBossesService
    {
        public async Task<List<BossModel>> RunAsync()
        {
            var bosses = mapper.Map<List<BossModel>>(await repository.GetAllBossesAsync());
            return bosses;
        }
    }
}
