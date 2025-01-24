using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    public class ProjectileCollisionHandler : ProjectileRequiredComponent
    {
        private ProjectileEvents projectileEvents;
        public override void Start()
        {
            //Initialize Projectile Collision
            ProjectileCollider projectileCollider = gameObject.AddComponent<ProjectileCollider>();
            projectileCollider.Initialize(weaponStatContainer.ProjectileRadius.TrueValue, true);

            projectileEvents = container.GetSerializableComponent<ProjectileEvents>();

            //Subscribe to events
            projectileCollider.projectileCollisionEnter += OnProjectileCollisionEnter;
            projectileCollider.projectileCollisionStay += OnProjectileCollisionStay;
            projectileCollider.projectileCollisionExit += OnProjectileCollisionExit;

            projectileCollider.projectileTriggerEnter += OnProjectileTriggerEnter;
            projectileCollider.projectileTriggerStay += OnProjectileTriggerStay;
            projectileCollider.projectileTriggerExit += OnProjectileTriggerExit;
        }

        private void OnProjectileTriggerEnter(Collider collider)
        {
            projectileEvents.projectileTriggerEnter?.Invoke(collider);
        }

        private void OnProjectileTriggerStay(Collider collider)
        {
            projectileEvents.projectileTriggerStay?.Invoke(collider);
        }

        private void OnProjectileTriggerExit(Collider collider)
        {
            projectileEvents.projectileTriggerExit?.Invoke(collider);
        }

        private void OnProjectileCollisionEnter(Collision collision)
        {
            projectileEvents.projectileCollisionEnter?.Invoke(collision);
        }

        private void OnProjectileCollisionStay(Collision collision)
        {
            projectileEvents.projectileCollisionStay?.Invoke(collision);
        }

        private void OnProjectileCollisionExit(Collision collision)
        {
            projectileEvents.projectileCollisionExit?.Invoke(collision);
        }
    }
}
