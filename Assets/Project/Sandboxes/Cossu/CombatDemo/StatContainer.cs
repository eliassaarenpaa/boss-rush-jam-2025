using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StatContainer
{
    abstract public List<Stat> GetStats();
}

[Serializable]
public class ProjectileStats : StatContainer
{
    public Stat projectileDamage = new Stat(StatType.ProjectileDamage);
    public Stat projectileSpeed = new Stat(StatType.ProjectileSpeed);
    public Stat projectileRadius = new Stat(StatType.ProjectileRadius);
    public Stat projectileRicochetAmount = new Stat(StatType.ProjectileRicochetAmount);
    public Stat projectileAmount = new Stat(StatType.ProjectileAmount);
    public Stat projectileSpread = new Stat(StatType.ProjectileSpread);

    override public List<Stat> GetStats()
    {
        List<Stat> stats = new List<Stat>()
        {
            projectileDamage,
            projectileSpeed,
            projectileRadius,
            projectileRicochetAmount,
            projectileAmount,
            projectileSpread
        };

        return stats;
    }
}