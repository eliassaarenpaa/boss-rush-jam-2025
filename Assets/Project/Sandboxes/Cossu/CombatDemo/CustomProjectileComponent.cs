using System;
using UnityEngine;

[Serializable]
public abstract class CustomProjectileComponent : SerializableMonobehaviour
{
    [NonSerialized] public TarotDataScriptable tarotData; //Projectile Base Data
}