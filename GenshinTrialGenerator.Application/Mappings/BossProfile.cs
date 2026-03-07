using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Boss;
using GenshinTrialGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Mappings.AutoMapperProfiles
{
    public class BossProfile : Profile
    {
        public BossProfile()
        {
            CreateMap<BossEntity, BossModel>();
            CreateMap<BossModel, BossEntity>();
        }
    }
}
