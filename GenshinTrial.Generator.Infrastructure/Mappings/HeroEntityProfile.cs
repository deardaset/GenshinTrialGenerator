using AutoMapper;
using GenshinTrialGenerator.Core.Models;
using GenshinTrialGenerator.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Infrastructure.Mappings
{
    public class HeroEntityProfile : Profile
    {
        public HeroEntityProfile()
        {
            CreateMap<HeroEntity, Hero>();
            CreateMap<Hero, HeroEntity>();
        }
    }
}
