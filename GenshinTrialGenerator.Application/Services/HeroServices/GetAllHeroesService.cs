using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Application.Interfaces.HeroServices;
using GenshinTrialGenerator.Core.Entities;
using GenshinTrialGenerator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.HeroServices
{
    public class GetAllHeroesService(IHeroRepository repository, IMapper mapper) : IGetAllHeroesService
    {
        public async Task<List<HeroModel>> RunAsync()
        {
            var heroes = mapper.Map<List<HeroModel>>(await repository.GetAllHeroesAsync());
            return heroes;
        }
    }
}
