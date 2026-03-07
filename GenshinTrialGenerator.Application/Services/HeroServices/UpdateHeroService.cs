using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Application.Interfaces.HeroServices;
using GenshinTrialGenerator.Core.Interfaces;
using GenshinTrialGenerator.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.HeroServices
{
    public class UpdateHeroService(IHeroRepository repository) : IUpdateHeroService
    {
        public async Task<HeroModel> RunAsync(Guid guid, UpdateHeroRequest request)
        {
            var hero = await repository.GetHeroAsync(guid);
            if (hero == null)
                throw new GeneratorNotFoundException("Hero not found");

            bool hasChanges = false;

            if (request.Name is { } name && name != hero.Name) //Name
                (hero.Name, hasChanges) = (name, true);
            if (request.Description is { } description && description != hero.Description) //Description
                (hero.Description, hasChanges) = (description, true);
            if (request.Rarity is { } rarity && rarity != hero.Rarity) //Rarity
                (hero.Rarity, hasChanges) = (rarity, true);
            if (request.Weapon is { } weapon && weapon != hero.Weapon) //Weapon
                (hero.Weapon, hasChanges) = (weapon, true);
            if (request.Element is { } element && element != hero.Element) //Element
                (hero.Element, hasChanges) = (element, true);
            if (request.Model is { } model && model != hero.Model) //Model
                (hero.Model, hasChanges) = (model, true);
            if (request.TeamBonus is { } teamBonus && teamBonus != hero.TeamBonus) //TeamBonus
                (hero.TeamBonus, hasChanges) = (teamBonus, true);
            if (request.Role is { } role && role != hero.Role) //Role
                (hero.Role, hasChanges) = (role, true);

            if (hasChanges)
                await repository.UpdateHeroAsync(hero);

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
