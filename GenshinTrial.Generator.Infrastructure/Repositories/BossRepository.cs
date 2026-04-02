using GenshinTrial.Generator.Infrastructure.Data;
using GenshinTrialGenerator.Core.Models;
using GenshinTrialGenerator.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GenshinTrialGenerator.Infrastructure.Entities;
using GenshinTrialGenerator.Core.Enums;

namespace GenshinTrialGenerator.Infrastructure.Repositories
{
    public class BossRepository(AppDbContext context, IMapper mapper) : IBossRepository
    {
        public async Task CreateBossAsync(Boss boss)
        {
            context.Bosses.Add(mapper.Map<BossEntity>(boss));
            await context.SaveChangesAsync();
        }

        public async Task DeleteBossAsync(Boss boss)
        {
            context.Bosses.Remove(mapper.Map<BossEntity>(boss));
            await context.SaveChangesAsync();
        }

        public async Task<(List<Boss>, int total)> GetAllBossesAsync(int page, int pageSize, string? search, string? sort, string? element)
        {
            var query = context.Bosses.AsNoTracking();

            query = ApplyFilters(query, search, sort, element);

            var total = await query.CountAsync();

            var bosses = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (mapper.Map<List<Boss>>(bosses), total);
        }

        public async Task<Boss?> GetBossAsync(Guid guid)
        {
            return mapper.Map<Boss>(await context.Bosses.AsNoTracking().FirstOrDefaultAsync(b => b.Guid == guid));
        }

        public async Task UpdateBossAsync(Boss boss)
        {
            context.Bosses.Update(mapper.Map<BossEntity>(boss));
            await context.SaveChangesAsync();
        }

        private static IQueryable<BossEntity> ApplyFilters(IQueryable<BossEntity> query, string? search, string? sort, string? element)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var term = $"%{search.Trim()}%";

                query = query.Where(b =>
                    EF.Functions.ILike(b.Name, term)
                );
            }

            if (!string.IsNullOrEmpty(element) && Enum.TryParse<ElementType>(element, ignoreCase: true, out var parsedElement))
            {
                query = query.Where(b => b.DamageType == parsedElement);
            }

            query = sort?.ToLower() switch
            {
                "name" => query.OrderBy(b => b.Name),
                "rarity" => query.OrderBy(b => b.Category),
                _ => query
            };

            return query;
        }
    }
}
