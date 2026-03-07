using GenshinTrialGenerator.Application.DTOs.Boss;
using GenshinTrialGenerator.Application.Interfaces.BossServices;
using GenshinTrialGenerator.Core.Entities;
using GenshinTrialGenerator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.BossServices
{
    public class CreateBossService(IBossRepository repository) : ICreateBossService
    {
        public async Task<BossModel> RunAsync(CreateBossRequest request)
        {
            //Create
            var boss = new BossEntity
            {
                Name = request.Name,
                Description = request.Description,
                BossType = request.BossType,
                DamageType = request.DamageType,
                HasWeakPoint = request.HasWeakPoint,
                Region = request.Region,
                Location = request.Location,
                Category = request.Category
            };
            await repository.CreateBossAsync(boss);

            //Response
            return new BossModel
            {
                Guid = boss.Guid,
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
