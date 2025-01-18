using System;
using UnityEngine;

[Serializable]
public abstract class SerializableProjectileComponent : SerializableMonobehaviour
{
    [NonSerialized] public TarotDataScriptable tarotData; //Projectile Base Data
}