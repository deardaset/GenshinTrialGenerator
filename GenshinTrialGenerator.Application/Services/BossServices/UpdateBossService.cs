using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Boss;
using GenshinTrialGenerator.Application.Interfaces.BossServices;
using GenshinTrialGenerator.Core.Interfaces;
using GenshinTrialGenerator.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.BossServices
{
    public class UpdateBossService(IBossRepository repository, IMapper mapper) : IUpdateBossService
    {
        public async Task<BossDto> RunAsync(Guid guid, UpdateBossRequest request)
        {
            var boss = await repository.GetBossAsync(guid);
            if (boss == null)
                throw new GeneratorNotFoundException("Boss not found");

            boss.Update(
                name: request.Name,
                description: request.Description,
                bossType: request.BossType,
                damageType: request.DamageType,
                hasWeakPoint: request.HasWeakPoint,
                region: request.Region,
                location: request.Location,
                category: request.Category
                );

            await repository.UpdateBossAsync(boss);

            return mapper.Map<BossDto>(boss);
        }
    }
}
