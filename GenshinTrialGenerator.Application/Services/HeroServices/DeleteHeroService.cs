using GenshinTrialGenerator.Application.Interfaces.HeroServices;
using GenshinTrialGenerator.Core.Interfaces;
using GenshinTrialGenerator.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.HeroServices
{
    public class DeleteHeroService(IHeroRepository repository) : IDeleteHeroService
    {
        public async Task RunAsync(Guid guid)
        {
            var hero = await repository.GetHeroAsync(guid);

            if (hero == null)
                throw new GeneratorNotFoundException("Hero not found");

            await repository.DeleteHeroAsync(hero);
        }
    }
}
