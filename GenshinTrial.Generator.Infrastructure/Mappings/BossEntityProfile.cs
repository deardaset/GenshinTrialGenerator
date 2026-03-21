using AutoMapper;
using GenshinTrialGenerator.Core.Models;
using GenshinTrialGenerator.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Infrastructure.Mappings
{
    public class BossEntityProfile : Profile
    {
        public BossEntityProfile()
        {
            CreateMap<BossEntity, Boss>();
            CreateMap<Boss, BossEntity>();
        }
    }
}
