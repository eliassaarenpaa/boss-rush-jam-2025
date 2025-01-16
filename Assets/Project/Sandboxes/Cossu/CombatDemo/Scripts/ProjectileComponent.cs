using System;
using UnityEngine;

public abstract class ProjectileComponent : MonoBehaviour
{
    public TarotDataObject tarotDataObject;
    public ProjectileEvents projectileEvents;

    public virtual void Setup() //Called by projectile assembler after dependencies have been injected
    {

    }
}
