using System;
using UnityEngine;
using System.Collections.Generic;

//This represents a stat and holds all modifiers applied to it.
//TrueValue is used for fetching the value with modifiers applied.
[Serializable]
public class Stat 
{
    public StatType type { get; private set; }
    public float baseValue;
    public float TrueValue => CalculateTrueValue();
    private List<Modifier> multiplicativeModifiers;
    private List<Modifier> additiveModifiers;
    private List<Modifier> flatModifiers;

    public Stat(StatType _type)
    {
        type = _type;
    }

    private float CalculateTrueValue()
    {
        float finalValue = baseValue;

        foreach (Modifier modifier in flatModifiers) //First add flat mods
        {
            finalValue += modifier.Value;
        }
        foreach (Modifier modifier in multiplicativeModifiers)
        {
            finalValue += finalValue * (modifier.Value / 100);
        }
        foreach (Modifier modifier in additiveModifiers) //At last add additive
        {
            finalValue *= modifier.Value / 100;
        }
        return finalValue;
    }

    public void AddModifier(Modifier modifier)
    {
        switch (modifier.modifierType)
        {
            case ModifierType.Multiplicative:
                multiplicativeModifiers.Add(modifier);
                break;
            case ModifierType.Additive:
                additiveModifiers.Add(modifier);
                break;
            case ModifierType.Flat:
                flatModifiers.Add(modifier);
                break;
        }
    }
}

public enum StatType
{
    MagazineSize,
    FireRate,
    ReloadTime,
    ProjectileDamage,
    ProjectileSpeed,
    ProjectileRadius,
    ProjectileRicochetAmount,
    ProjectileAmount,
    ProjectileSpread
}