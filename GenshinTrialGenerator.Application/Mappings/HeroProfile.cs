using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Mappings
{
    public class HeroProfile : Profile
    {
        public HeroProfile()
        {
            CreateMap<HeroEntity, HeroModel>();
            CreateMap<HeroModel, HeroEntity>();
        }
    }
}
