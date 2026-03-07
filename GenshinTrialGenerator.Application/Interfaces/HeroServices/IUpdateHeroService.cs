using GenshinTrialGenerator.Application.DTOs.Hero;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Interfaces.HeroServices
{
    public interface IUpdateHeroService
    {
        public Task<HeroModel> RunAsync(Guid guid, UpdateHeroRequest request);
    }
}
