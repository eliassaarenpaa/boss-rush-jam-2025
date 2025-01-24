using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public WeaponStatContainer baseStats;
    [SerializeReference] public ProjectileMovementComponent projectileMovementComponent;
    [SerializeReference] public List<ProjectileCustomComponent> customComponents = new List<ProjectileCustomComponent>();
}