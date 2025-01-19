using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "TarotObject", menuName = "SO/TarotDataObject", order = 0)]
public class TarotDataScriptable : ScriptableObject
{
    public TarotBaseData tarotBaseData;
    [SerializeReference]
    public List<CustomProjectileComponent> customProjectileComponents = new List<CustomProjectileComponent>();
}

[System.Serializable]
public class TarotBaseData
{
    [Header("Basic Weapon Properties")]
    //Weapon stats
    public int magazineSize;
    public float fireRate;
    public float reloadTime;

    [Header("Basic Projectile Properties")]
    //Projectile Basic Stats
    public float projectileDamage;
    public float projectileSpeed;
    public float projectileRadius;
    public int projectileRicochetAmount;

    [Header("Multi Projectile Properties")]
    //Multi Projectile Stats
    public int projectileAmount;
    public float projectileSpread;
}
