using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


[CreateAssetMenu(fileName = "TarotData", menuName = "SO/TarotData")]
public class TarotDataObject : ScriptableObject
{
    public TarotBaseData tarotBaseData;
    public List<MonoScript> projectileComponents;
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
