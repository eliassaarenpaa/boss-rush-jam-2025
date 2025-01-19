using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    //This is the projectile collider class that will detect collisions and launch events based on the trigger/collision type.
    //ProjectileCollisionHandler(SerializableMono) listens to these events and forwards them to ProjectileEvents(SerializableMono).

    public delegate void ProjectileTriggerEnter(Collider collider);
    public ProjectileTriggerEnter projectileTriggerEnter;

    public delegate void ProjectileTriggerStay(Collider collider);
    public ProjectileTriggerStay projectileTriggerStay;

    public delegate void ProjectileTriggerExit(Collider collider);
    public ProjectileTriggerExit projectileTriggerExit;

    public delegate void ProjectileCollisionEnter(Collision collision);
    public ProjectileCollisionEnter projectileCollisionEnter;

    public delegate void ProjectileCollisionStay(Collision collision);
    public ProjectileCollisionStay projectileCollisionStay;

    public delegate void ProjectileCollisionExit(Collision collision);
    public ProjectileCollisionExit projectileCollisionExit;

    public void Initialize(float radius, bool isTrigger)
    {
        SphereCollider sphereCol = gameObject.AddComponent<SphereCollider>();
        sphereCol.radius = radius;
        sphereCol.isTrigger = isTrigger;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void OnTriggerEnter(Collider collider)
    {
        projectileTriggerEnter?.Invoke(collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        projectileTriggerStay?.Invoke(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        projectileTriggerExit?.Invoke(collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        projectileCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        projectileCollisionStay?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        projectileCollisionExit?.Invoke(collision);
    }
}
