using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(SerializableMonoBehaviourContainer))]
public class ProjectileAssembler : MonoBehaviour
{
    public void Assemble(TarotDataScriptable sourceData)
    {
        //Make a copy of the scriptable object.
        TarotDataScriptable localData = ScriptableObject.Instantiate(sourceData);

        //Get reference to the component container
        SerializableMonoBehaviourContainer componentContainer = GetComponent<SerializableMonoBehaviourContainer>();

        //Add required components
        List<RequiredProjectileComponent> requiredComponents = new RequiredProjectileComponents().components;

        foreach (RequiredProjectileComponent component in requiredComponents) //Data injection for each required projectile component
        {
            component.tarotData = localData;
            componentContainer.AddSerializableMonoBehaviour(component);
        }

        //Add Custom Components to the container
        foreach(CustomProjectileComponent component in localData.customProjectileComponents) //Data injection for each custom projectile component
        {
            component.tarotData = localData;
        }
        componentContainer.AddSerializableMonoBehaviour(localData.customProjectileComponents);

    }
}
