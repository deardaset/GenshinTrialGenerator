using AutoMapper;
using GenshinTrialGenerator.Application.DTOs.Boss;
using GenshinTrialGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Mappings
{
    public class BossProfile : Profile
    {
        public BossProfile()
        {
            CreateMap<Boss, BossDto>();
            CreateMap<BossDto, Boss>();
        }
    }
}
