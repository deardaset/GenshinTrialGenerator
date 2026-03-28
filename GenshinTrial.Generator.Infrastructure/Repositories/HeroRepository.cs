using AutoMapper;
using GenshinTrial.Generator.Infrastructure.Data;
using GenshinTrialGenerator.Core.Enums;
using GenshinTrialGenerator.Core.Interfaces;
using GenshinTrialGenerator.Core.Models;
using GenshinTrialGenerator.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GenshinTrialGenerator.Infrastructure.Repositories
{
    public class HeroRepository(AppDbContext context, IMapper mapper) : IHeroRepository
    {
        public async Task CreateHeroAsync(Hero hero)
        {
            context.Heroes.Add(mapper.Map<HeroEntity>(hero));
            await context.SaveChangesAsync();
        }

        public async Task DeleteHeroAsync(Hero hero)
        {
            context.Heroes.Remove(mapper.Map<HeroEntity>(hero));
            await context.SaveChangesAsync();
        }

        public async Task<(List<Hero>, int total)> GetAllHeroesAsync(int page, int pageSize, string? search, string? sort, string? element)
        {
            var query = context.Heroes.AsNoTracking();

            query = ApplyFilters(query, search, sort, element);

            var total = await query.CountAsync();

            var heroes = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (mapper.Map<List<Hero>>(heroes), total);
        }

        public async Task<Hero?> GetHeroAsync(Guid guid)
        {
            return mapper.Map<Hero>(await context.Heroes.AsNoTracking().FirstOrDefaultAsync(h => h.Guid == guid));
        }

        public async Task UpdateHeroAsync(Hero hero)
        {
            context.Heroes.Update(mapper.Map<HeroEntity>(hero));
            await context.SaveChangesAsync();
        }

        private static IQueryable<HeroEntity> ApplyFilters(IQueryable<HeroEntity> query, string? search, string? sort, string? element)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var term = $"%{search.Trim()}%";

                query = query.Where(h =>
                    EF.Functions.ILike(h.Name, term)
                );
            }

            if (!string.IsNullOrEmpty(element) && Enum.TryParse<ElementType>(element, ignoreCase: true, out var parsedElement))
            {
                query = query.Where(h => h.Element == parsedElement);
            }

            query = sort?.ToLower() switch
            {
                "name" => query.OrderBy(h => h.Name),
                "rarity" => query.OrderBy(h => h.Rarity),
                _ => query
            };

            return query;
        }
    }
}
