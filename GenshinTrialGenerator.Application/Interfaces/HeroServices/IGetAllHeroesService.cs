using GenshinTrialGenerator.Application.DTOs;
using GenshinTrialGenerator.Application.DTOs.Hero;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Interfaces.HeroServices
{
    public interface IGetAllHeroesService
    {
        public Task<PagedResponse<HeroDto>> RunAsync(GetDataOptionsRequest request);
    }
}
