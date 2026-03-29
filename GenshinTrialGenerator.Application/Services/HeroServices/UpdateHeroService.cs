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
    public class UpdateHeroService(IHeroRepository repository, IStorageService storage, IMapper mapper) : IUpdateHeroService
    {
        public async Task<HeroDto> RunAsync(Guid guid, UpdateHeroRequest request)
        {
            var hero = await repository.GetHeroAsync(guid);
            if (hero == null)
                throw new GeneratorNotFoundException("Hero not found");

            string? photoUrl = hero.PhotoUrl;
            if (request.Photo != null)
            {
                if (!string.IsNullOrEmpty(hero.PhotoUrl))
                    await storage.DeleteAsync(hero.PhotoUrl);

                photoUrl = await storage.UploadAsync(request.Photo.OpenReadStream(), request.Photo.FileName, request.Photo.ContentType, "heroes");
            }

            hero.Update(
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
            
            await repository.UpdateHeroAsync(hero);

            return mapper.Map<HeroDto>(hero);
        }
    }
}
