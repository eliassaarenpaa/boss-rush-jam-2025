using UnityEngine;

/*
 *This script is solely for adding and configuring a collider. For this the required components are a sphere collider and a rigidbody. Nothing else
 *The reason we do not configure the rigidbody here is because we might want different configurations for the gravity depending on the movement
 *that is why the responsibility for configuring the rigidbody is on other components like movement.
*/

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class ProjectileCollision : MonoBehaviour
{
    ProjectileEvents projectileEvents;
    bool configured;
    public void Configure(TarotDataObject tarotDataObject, ProjectileEvents pEvents)
    {
        //Setup collider
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.radius = tarotDataObject.tarotBaseData.projectileRadius;
        collider.isTrigger = false;
        
        //Setup Rigidbody collision detection mode
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        
        //Add reference to projectile Events
        projectileEvents = pEvents;
        configured = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!configured) return;
        projectileEvents.onProjectileCollisionEnter?.Invoke(collision);
    }
}
