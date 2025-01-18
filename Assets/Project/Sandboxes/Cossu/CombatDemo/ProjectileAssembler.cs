using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SerializableMonoBehaviourContainer))]
public class ProjectileAssembler : MonoBehaviour
{
    public void Assemble(TarotDataScriptable sourceData)
    {
        TarotDataScriptable localData = ScriptableObject.Instantiate(sourceData); //Make a copy of the scriptable object
        SerializableMonoBehaviourContainer componentContainer = GetComponent<SerializableMonoBehaviourContainer>();

        //Data injection for each projectile component
        foreach(SerializableProjectileComponent component in localData.projectileComponents)
        {
            component.tarotData = localData;
        }

        //Add Components To The Container
        componentContainer.AddSerializableMonoBehaviour(localData.projectileComponents);
    }
}
