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
    public class CreateHeroService(IHeroRepository repository, IStorageService storage, IMapper mapper) : ICreateHeroService
    {
        public async Task<HeroDto> RunAsync(CreateHeroRequest request)
        {
            string? photoUrl = null;
            if (request.Photo != null)
                photoUrl = await storage.UploadAsync(request.Photo.OpenReadStream(), request.Photo.FileName, request.Photo.ContentType, "heroes");

            var hero = new Hero(
                name: request.Name,
                description: request.Description,
                rarity: request.Rarity,
                weapon: request.Weapon,
                element: request.Element,
                model: request.Model,
                teamBonus: request.TeamBonus,
                role: request.Role,
                photoUrl: photoUrl
                );

            await repository.CreateHeroAsync(hero);

            return mapper.Map<HeroDto>(hero);
        }
    }
}
