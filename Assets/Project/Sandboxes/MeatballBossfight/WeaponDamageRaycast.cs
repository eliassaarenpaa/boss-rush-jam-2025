using Project.Runtime.Core.Input;
using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight
{
    public class WeaponDamageRaycast : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private int damage;
        [SerializeField] private float range;
        [SerializeField] private LayerMask hitLayer;

        private void Update()
        {
            if (PlayerInput.Attack)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            var dir = Camera.main.transform.forward; 

            if (Physics.Raycast(origin.position, dir, out var hit, range, hitLayer))
            {
                var damageable = hit.collider?.transform.parent?.parent?.GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                }
            }
            
            Debug.DrawRay(origin.position, dir * range, Color.red, 1f);
        }
    }
}
