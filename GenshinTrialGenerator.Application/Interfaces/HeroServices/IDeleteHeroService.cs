using GenshinTrialGenerator.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Interfaces.HeroServices
{
    public interface IDeleteHeroService
    {
        public Task RunAsync(Guid guid);
    }
}
