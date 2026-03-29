using GenshinTrialGenerator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Core.Models
{
    public class Boss
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public BossType BossType { get; private set; }
        public ElementType DamageType { get; private set; }
        public bool HasWeakPoint { get; private set; }
        public RegionType Region { get; private set; }
        public string? Location { get; private set; }
        public BossCategoryType Category { get; private set; }
        public string? PhotoUrl { get; private set; }

        public Boss(
            string name, 
            BossType bossType, 
            ElementType damageType, 
            bool hasWeakPoint, 
            RegionType region,
            BossCategoryType category, 
            string? description = null, 
            string? location = null,
            string? photoUrl = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is required");

            Name = name;
            BossType = bossType;
            DamageType = damageType;
            HasWeakPoint = hasWeakPoint;
            Region = region;
            Category = category;
            Description = description;
            Location = location;
            PhotoUrl = photoUrl;
        }

        public void Update(
            string? name = null,
            string? description = null,
            string? location = null,
            BossType? bossType = null,
            ElementType? damageType = null,
            bool? hasWeakPoint = null,
            RegionType? region = null,
            BossCategoryType? category = null,
            string? photoUrl = null)
        {
            if (name is not null) Name = name;
            if (description is not null) Description = description;
            if (bossType is not null) BossType = bossType.Value;
            if (damageType is not null) DamageType = damageType.Value;
            if (hasWeakPoint is not null) HasWeakPoint = hasWeakPoint.Value;
            if (region is not null) Region = region.Value;
            if (location is not null) Location = location;
            if (category is not null) Category = category.Value;
            if (photoUrl is not null) PhotoUrl = photoUrl;
        }
    }
}
