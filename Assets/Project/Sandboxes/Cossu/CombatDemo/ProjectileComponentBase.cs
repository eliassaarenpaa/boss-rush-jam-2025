using System;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    [Serializable]
    public abstract class ProjectileComponentBase : SerializableMonobehaviour
    {
        [NonSerialized] public WeaponStatContainer weaponStatContainer;
    }
}