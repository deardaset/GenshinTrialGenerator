using GenshinTrialGenerator.Application.Interfaces.BossServices;
using GenshinTrialGenerator.Core.Interfaces;
using GenshinTrialGenerator.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.BossServices
{
    public class DeleteBossService(IBossRepository repository) : IDeleteBossService
    {
        public async Task RunAsync(Guid guid)
        {
            var boss = await repository.GetBossAsync(guid);

            if (boss == null)
                throw new GeneratorNotFoundException("Hero not found");

            await repository.DeleteBossAsync(boss);
        }
    }
}
