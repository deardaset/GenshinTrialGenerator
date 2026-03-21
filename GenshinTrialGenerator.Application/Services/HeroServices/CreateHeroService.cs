using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Application.Interfaces.HeroServices;
using GenshinTrialGenerator.Core.Models;
using GenshinTrialGenerator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.HeroServices
{
    public class CreateHeroService(IHeroRepository repository, IMapper mapper) : ICreateHeroService
    {
        public async Task<HeroDto> RunAsync(CreateHeroRequest request)
        {
            var hero = new Hero(
                name: request.Name,
                description: request.Description,
                rarity: request.Rarity,
                weapon: request.Weapon,
                element: request.Element,
                model: request.Model,
                teamBonus: request.TeamBonus,
                role: request.Role
                );

            await repository.CreateHeroAsync(hero);

            return mapper.Map<HeroDto>(hero);
        }
    }
}
