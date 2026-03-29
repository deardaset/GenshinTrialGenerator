using AutoMapper;
using GenshinTrialGenerator.Application.DTOs;
using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Application.Interfaces.HeroServices;
using GenshinTrialGenerator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.HeroServices
{
    public class GetAllHeroesService(IHeroRepository repository, IMapper mapper) : IGetAllHeroesService
    {
        public async Task<PagedResponse<HeroDto>> RunAsync(GetDataOptionsRequest request)
        {
            var (heroes, total) = await repository.GetAllHeroesAsync(request.Page, request.PageSize, request.Search, request.Sort, request.Element);
            Console.WriteLine($"-------HEROES COUNT: {heroes?.Count()}");
            var heroesDto = mapper.Map<List<HeroDto>>(heroes);

            return new PagedResponse<HeroDto>
            {
                Items = heroesDto,
                TotalCount = total
            };
        }
    }
}
