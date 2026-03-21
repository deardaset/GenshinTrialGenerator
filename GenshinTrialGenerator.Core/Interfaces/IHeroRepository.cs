using GenshinTrialGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Core.Interfaces
{
    public interface IHeroRepository
    {
        public Task CreateHeroAsync(Hero hero);
        public Task<List<Hero>> GetAllHeroesAsync();
        public Task<Hero?> GetHeroAsync(Guid guid);
        public Task UpdateHeroAsync(Hero hero);
        public Task DeleteHeroAsync(Hero hero);
    }
}
