using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(SerializableMonoBehaviourContainer))]
public class ProjectileAssembler : MonoBehaviour
{
    public void Assemble(WeaponData sourceData)
    {
        //Get reference to the component container
        SerializableMonoBehaviourContainer componentContainer = GetComponent<SerializableMonoBehaviourContainer>();

        //Add required components
        List<ProjectileRequiredComponent> requiredComponents = RequiredProjectileComponents.components;
        foreach (ProjectileRequiredComponent component in requiredComponents) //Data injection for each required projectile component
        {
            component.weaponStatContainer = sourceData.baseStats;
            componentContainer.AddSerializableMonoBehaviour(component);
        }

        //Add Movement Component
        if (sourceData.projectileMovementComponent != null)
        {
            sourceData.projectileMovementComponent.weaponStatContainer = sourceData.baseStats;
            componentContainer.AddSerializableMonoBehaviour(sourceData.projectileMovementComponent);
        }

        //Add Custom Components to the container
        foreach(ProjectileCustomComponent component in sourceData.customComponents) //Data injection for each custom projectile component
        {
            component.weaponStatContainer = sourceData.baseStats;
        }
        componentContainer.AddSerializableMonoBehaviour(sourceData.customComponents);
    }
}
