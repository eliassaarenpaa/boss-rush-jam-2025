using System;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    [Serializable]
    public abstract class ProjectileCustomComponent : ProjectileComponentBase
    {
        [NonSerialized] public TarotDataScriptable tarotData; //Projectile Base Data
    }
}