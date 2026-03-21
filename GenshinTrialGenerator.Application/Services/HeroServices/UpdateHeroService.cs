using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Application.Interfaces.HeroServices;
using GenshinTrialGenerator.Core.Interfaces;
using GenshinTrialGenerator.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.HeroServices
{
    public class UpdateHeroService(IHeroRepository repository, IMapper mapper) : IUpdateHeroService
    {
        public async Task<HeroDto> RunAsync(Guid guid, UpdateHeroRequest request)
        {
            var hero = await repository.GetHeroAsync(guid);
            if (hero == null)
                throw new GeneratorNotFoundException("Hero not found");

            hero.Update(
                name: request.Name, 
                description: request.Description,
                rarity: request.Rarity,
                weapon: request.Weapon,
                element: request.Element,
                model: request.Model,
                teamBonus: request.TeamBonus,
                role: request.Role
                );
            
            await repository.UpdateHeroAsync(hero);

            return mapper.Map<HeroDto>(hero);
        }
    }
}
