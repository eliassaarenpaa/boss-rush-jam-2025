using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

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

//This represents a stat and holds all modifiers applied to it.
//TrueValue is used for fetching the value with modifiers applied.
[Serializable]
public class Stat 
{
    public Stat(StatType statType, StatContainer container)
    {
        _statType = statType;
        container.AddStat(this);
    }

    [SerializeField]
    [ReadOnly]
    private StatType _statType;
    public StatType StatType => _statType;

    public float baseValue;
    public float TrueValue => CalculateTrueValue();
    [SerializeField]
    private HashSet<Modifier> multiplicativeModifiers = new HashSet<Modifier>();
    [SerializeField]
    private HashSet<Modifier> additiveModifiers = new HashSet<Modifier>();
    [SerializeField]
    private HashSet<Modifier> flatModifiers = new HashSet<Modifier>();

    private float CalculateTrueValue()
    {
        float finalValue = baseValue;

        foreach (Modifier modifier in flatModifiers)
        {
            finalValue += modifier.Value; //Add flat to base value
        }
        foreach (Modifier modifier in multiplicativeModifiers)
        {
            finalValue += finalValue * (modifier.Value / 100); //Multiply new base value
        }
        foreach (Modifier modifier in additiveModifiers)
        {
            finalValue *= modifier.Value / 100; //Multiply after all modifiers
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

    public void RemoveModifier(Modifier modifier)
    {
        switch (modifier.modifierType)
        {
            case ModifierType.Multiplicative:
                if (multiplicativeModifiers.Contains(modifier))
                {
                    multiplicativeModifiers.Remove(modifier);
                }
                break;
            case ModifierType.Additive:
                if(additiveModifiers.Contains(modifier))
                {
                    additiveModifiers.Remove(modifier);
                }
                break;
            case ModifierType.Flat:
                if (flatModifiers.Contains(modifier))
                {
                    flatModifiers.Remove(modifier);
                }
                break;
        }
    }

    public void ClearModifiers()
    {
        multiplicativeModifiers.Clear();
        additiveModifiers.Clear();
        flatModifiers.Clear();
    }
}