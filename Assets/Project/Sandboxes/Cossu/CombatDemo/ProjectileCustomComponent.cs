using System;
using UnityEngine;

[Serializable]
public abstract class ProjectileCustomComponent : ProjectileComponentBase
{
    [NonSerialized] public TarotDataScriptable tarotData; //Projectile Base Data
}