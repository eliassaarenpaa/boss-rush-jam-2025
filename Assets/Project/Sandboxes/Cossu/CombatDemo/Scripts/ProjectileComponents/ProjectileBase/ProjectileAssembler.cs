using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(ProjectileEvents))]
public class ProjectileAssembler : MonoBehaviour
{
    public void AssembleProjectile(TarotDataObject tarotDataObject)
    {
        //Get Events Component
        ProjectileEvents pEvents = GetComponent<ProjectileEvents>();

        //Add additional projectile components from the tarot data object
        foreach (MonoScript monoScript in tarotDataObject.projectileComponents)
        {
            if (monoScript == null) continue;

            var monoScriptType = monoScript.GetClass();
            if(typeof(ProjectileComponent).IsAssignableFrom(monoScriptType))
            {
                ProjectileComponent pComponent = (ProjectileComponent)gameObject.AddComponent(monoScriptType);
                pComponent.Setup(tarotDataObject, pEvents);
            }
        }
    }
}
