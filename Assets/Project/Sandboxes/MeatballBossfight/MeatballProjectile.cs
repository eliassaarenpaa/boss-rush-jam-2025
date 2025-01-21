using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Sandboxes.MeatballBossfight
{
    public class MeatballProjectile : MonoBehaviour
    {
        [SerializeField] private float projectileSpeed;
        [SerializeField] private int minDmg = 15, maxDmg = 25;
        [SerializeField] private LayerMask playerDamageableLayer;
        [SerializeField] private LayerMask enemyDamageableLayer;
        [SerializeField] private LayerMask projectileLayers;
        [SerializeField] private Rigidbody rb;

        private Collider _shooterCollider;

        public void Move(Vector3 dir)
        {
            rb.linearVelocity = dir * projectileSpeed;
        }

        public void SetShooterCollider(Collider shooterCollider)
        {
            _shooterCollider = shooterCollider;
        }

        private void OnCollisionEnter(Collision other)
        {
            // If projectile, ignore collision
            if( projectileLayers == (projectileLayers | (1 << other.gameObject.layer)) )
            {
                return;
            }
            
            // If shooter, ignore collision
            if( _shooterCollider != null && other.collider == _shooterCollider )
            {
                return;
            }
            
            // If player, deal damage and destroy projectile
            if (playerDamageableLayer == (playerDamageableLayer | (1 << other.gameObject.layer)))
            {
                other.gameObject.GetComponent<Damageable>().TakeDamage(Random.Range(minDmg, maxDmg));
            }
            
            Destroy(gameObject);
        }
    }
}
