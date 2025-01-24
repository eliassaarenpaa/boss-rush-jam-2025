using System;
using UnityEngine;

public abstract class ProjectileComponentBase : SerializableMonobehaviour
{
    [NonSerialized] public WeaponStatContainer weaponStatContainer;
}