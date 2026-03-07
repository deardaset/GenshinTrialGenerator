using GenshinTrialGenerator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.DTOs.Boss
{
    public class UpdateBossRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public BossType BossType { get; set; }
        public ElementType DamageType { get; set; }
        public bool HasWeakPoint { get; set; }
        public RegionType Region { get; set; }
        public string? Location { get; set; }
        public BossCategoryType Category { get; set; }
    }
}
