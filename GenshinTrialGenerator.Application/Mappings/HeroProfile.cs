using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Mappings
{
    public class HeroProfile : Profile
    {
        public HeroProfile()
        {
            CreateMap<Hero, HeroDto>();
            CreateMap<HeroDto, Hero>();
        }
    }
}
