using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Application.Interfaces.HeroServices;
using GenshinTrialGenerator.Core.Entities;
using GenshinTrialGenerator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.HeroServices
{
    public class CreateHeroService(IHeroRepository repository) : ICreateHeroService
    {
        public async Task<HeroModel> RunAsync(CreateHeroRequest request)
        {
            //Create
            var hero = new HeroEntity
            {
                Name = request.Name,
                Description = request.Description,
                Rarity = request.Rarity,
                Weapon = request.Weapon,
                Element = request.Element,
                Model = request.Model,
                TeamBonus = request.TeamBonus,
                Role = request.Role
            };
            await repository.CreateHeroAsync(hero);

            //Response
            return new HeroModel
            {
                Guid = hero.Guid,
                Name = hero.Name,
                Description = hero.Description,
                Rarity = hero.Rarity,
                Weapon = hero.Weapon,
                Element = hero.Element,
                Model = hero.Model,
                TeamBonus = hero.TeamBonus,
                Role = hero.Role
            };
        }
    }
}
