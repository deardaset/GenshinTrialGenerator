using GenshinTrialGenerator.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GenshinTrialGenerator.Core.Models
{
    public class Hero
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public HeroRarityType Rarity { get; private set; }
        public HeroWeaponType Weapon { get; private set; }
        public ElementType Element { get; private set; }
        public HeroModelType Model { get; private set; }
        public HeroTeamBonusType? TeamBonus { get; private set; }
        public HeroRolesType Role { get; private set; }
        public string? PhotoUrl { get; private set; }

        public Hero(
            string name,
            HeroRarityType rarity,
            HeroWeaponType weapon,
            ElementType element,
            HeroModelType model,
            HeroTeamBonusType? teamBonus,
            HeroRolesType role,
            string? description = null,
            string? photoUrl = null)
        {
            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException("Name is required");

            Name = name;
            Description = description;
            Rarity = rarity;
            Weapon = weapon;
            Element = element;
            Model = model;
            TeamBonus = teamBonus;
            Role = role;
            PhotoUrl = photoUrl;
        }

        public void Update(
            string? name = null,
            string? description = null,
            HeroRarityType? rarity = null,
            HeroWeaponType? weapon = null,
            ElementType? element = null,
            HeroModelType? model = null,
            HeroTeamBonusType? teamBonus = null,
            HeroRolesType? role = null,
            string? photoUrl = null)
        {
            if (name is not null) Name = name;
            if (description is not null) Description = description;
            if (rarity is not null) Rarity = rarity.Value;
            if (weapon is not null) Weapon = weapon.Value;
            if (element is not null) Element = element.Value;
            if (model  is not null) Model = model.Value;
            if (teamBonus is not null) TeamBonus = teamBonus.Value;
            if (role  is not null) Role = role.Value;
            if (photoUrl is not null) PhotoUrl = photoUrl;
        }
    }
}
