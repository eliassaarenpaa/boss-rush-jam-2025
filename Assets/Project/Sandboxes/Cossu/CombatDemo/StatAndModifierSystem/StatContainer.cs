using System.Collections.Generic;
using System;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    [Serializable]
    public abstract class StatContainer
    {
        private List<Stat> stats = new List<Stat>();
        public Stat StatQuery(StatType type) //Used for querying stats by type
        {
            foreach (Stat stat in stats)
            {
                if (stat.StatType == type)
                {
                    return stat;
                }
            }
            return null;
        }

        public void AddModifiersToStats(List<Modifier> modifiers)
        {
            foreach (Modifier modifier in modifiers)
            {
                StatQuery(modifier.statType).AddModifier(modifier);
            }
        }

        public void RemoveModifiersFromStats(List<Modifier> modifiers)
        {
            foreach (Modifier modifier in modifiers)
            {
                StatQuery(modifier.statType).RemoveModifier(modifier);
            }
        }

        public void ClearModifiersFromStats()
        {
            foreach (Stat stat in stats)
            {
                stat.ClearModifiers();
            }
        }

        public void AddStat(Stat stat)
        {
            stats.Add(stat);
        }
    }

    [Serializable]
    public class WeaponStatContainer : StatContainer
    {
        public Stat ProjectileDamage;
        public Stat ProjectileSpeed;
        public Stat ProjectileRadius;
        public Stat ProjectileRicochetAmount;
        public Stat ProjectileAmount;
        public Stat ProjectileSpread;
        public Stat ReloadTime;
        public Stat FireRate;
        public Stat MagazineSize;
        public WeaponStatContainer()
        {
            ProjectileDamage = new Stat(StatType.ProjectileDamage, this);
            ProjectileSpeed = new Stat(StatType.ProjectileSpeed, this);
            ProjectileRadius = new Stat(StatType.ProjectileRadius, this);
            ProjectileRicochetAmount = new Stat(StatType.ProjectileRicochetAmount, this);
            ProjectileAmount = new Stat(StatType.ProjectileAmount, this);
            ProjectileSpread = new Stat(StatType.ProjectileSpread, this);
            ReloadTime = new Stat(StatType.ReloadTime, this);
            FireRate = new Stat(StatType.FireRate, this);
            MagazineSize = new Stat(StatType.MagazineSize, this);
        }
    }
}