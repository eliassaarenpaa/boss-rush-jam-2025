using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public abstract class RequiredProjectileComponent : SerializableMonobehaviour
{
    public TarotDataScriptable tarotData;
}

public class RequiredProjectileComponents
{
    //here we manage the required components on a projectile and the initialization order.
    //ProjectileAssembler creates a new instance of this class and adds the required components by this order
    public List<SerializableMonobehaviour> components = new List<RequiredProjectileComponent>
    {
        new ProjectileEvents(),
        new ProjectileCollisionHandler(),
    };
}