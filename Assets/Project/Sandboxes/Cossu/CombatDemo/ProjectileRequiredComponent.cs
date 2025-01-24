using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public abstract class ProjectileRequiredComponent : ProjectileComponentBase
{}

static public class RequiredProjectileComponents
{
    //here we manage the required components on a projectile and the initialization order.
    //ProjectileAssembler creates a new instance of this class and adds the required components by this order
    static public List<ProjectileRequiredComponent> components = new List<ProjectileRequiredComponent>
    {
        new ProjectileEvents(),
        new ProjectileCollisionHandler(),
    };
}