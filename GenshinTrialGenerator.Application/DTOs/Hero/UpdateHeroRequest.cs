using GenshinTrialGenerator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.DTOs.Hero
{
    public class UpdateHeroRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public HeroRarityType Rarity { get; set; }
        public HeroWeaponType Weapon { get; set; }
        public ElementType Element { get; set; }
        public HeroModelType Model { get; set; }
        public HeroTeamBonusType TeamBonus { get; set; }
        public HeroRolesType Role { get; set; }
    }
}
