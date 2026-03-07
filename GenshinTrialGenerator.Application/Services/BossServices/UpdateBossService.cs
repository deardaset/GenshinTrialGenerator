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
    public class UpdateBossService(IBossRepository repository) : IUpdateBossService
    {
        public async Task<BossModel> RunAsync(Guid guid, UpdateBossRequest request)
        {
            var boss = await repository.GetBossAsync(guid);
            if (boss == null)
                throw new GeneratorNotFoundException("Boss not found");

            bool hasChanges = false;

            if (request.Name is { } name && name != boss.Name) //Name
                (boss.Name, hasChanges) = (name, true);
            if (request.Description is { } description && description != boss.Description) //Description
                (boss.Description, hasChanges) = (description, true);
            if (request.BossType is { } bossType && bossType != boss.BossType) //BossType
                (boss.BossType, hasChanges) = (bossType, true);
            if (request.DamageType is { } damageType && damageType != boss.DamageType) //DamageType
                (boss.DamageType, hasChanges) = (damageType, true);
            if (request.HasWeakPoint is { } hasWeakPoint && hasWeakPoint != boss.HasWeakPoint) //HasWeakPoint
                (boss.HasWeakPoint, hasChanges) = (hasWeakPoint, true);
            if (request.Region is { } region && region != boss.Region) //Region
                (boss.Region, hasChanges) = (region, true);
            if (request.Location is { } location && location != boss.Location) //Location
                (boss.Location,  hasChanges) = (location, true);
            if (request.Category is { } category && category != boss.Category) //Category
                (boss.Category, hasChanges) = (category, true);

            if (hasChanges)
                await repository.UpdateBossAsync(boss);

            return new BossModel
            {
                Name = boss.Name,
                Description = boss.Description,
                BossType = boss.BossType,
                DamageType = boss.DamageType,
                HasWeakPoint = boss.HasWeakPoint,
                Region = boss.Region,
                Location = boss.Location,
                Category = boss.Category
            };
        }
    }
}
