using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Boss;
using GenshinTrialGenerator.Application.Interfaces.BossServices;
using GenshinTrialGenerator.Core.Models;
using GenshinTrialGenerator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.BossServices
{
    public class CreateBossService(IBossRepository repository, IMapper mapper) : ICreateBossService
    {
        public async Task<BossDto> RunAsync(CreateBossRequest request)
        {
            var boss = new Boss(
                name: request.Name,
                description: request.Description,
                bossType: request.BossType,
                damageType: request.DamageType,
                hasWeakPoint: request.HasWeakPoint,
                region: request.Region,
                location: request.Location,
                category: request.Category
                );

            await repository.CreateBossAsync(boss);

            return mapper.Map<BossDto>(boss);
        }
    }
}
