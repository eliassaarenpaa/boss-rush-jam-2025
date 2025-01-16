using UnityEngine;

/*
 *This script is solely for adding and configuring a collider. For this the required components are a sphere collider and a rigidbody. Nothing else
 *The reason we do not configure the rigidbody here is because we might want different configurations for the gravity depending on the movement
 *that is why the responsibility for configuring the rigidbody is on other components like movement.
*/

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class ProjectileCollision : ProjectileComponent
{
    public override void OnSetup()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.radius = base.tarotDataObject.tarotBaseData.projectileRadius;
        collider.isTrigger = false;

        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision enter");
        base.projectileEvents.onProjectileCollisionEnter?.Invoke(collision);
    }
}
