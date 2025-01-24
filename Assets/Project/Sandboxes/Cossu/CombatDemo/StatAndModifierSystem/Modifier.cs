using NUnit.Framework;
using System;

[Serializable]
public struct Modifier
{
    public StatType statType;
    public ModifierType modifierType;
    public float Value;
}

[Serializable]
public enum ModifierType
{
    Multiplicative,
    Additive,
    Flat
}