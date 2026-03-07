using GenshinTrialGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Core.Interfaces
{
    public interface IHeroRepository
    {
        public Task CreateHeroAsync(HeroEntity hero);
        public Task<List<HeroEntity>> GetAllHeroesAsync();
        public Task<HeroEntity?> GetHeroAsync(Guid guid);
        public Task UpdateHeroAsync(HeroEntity hero);
        public Task DeleteHeroAsync(HeroEntity hero);
    }
}
