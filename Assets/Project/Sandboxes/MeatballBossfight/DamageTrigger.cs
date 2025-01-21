using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight
{
    public class DamageTrigger : MonoBehaviour
    {
        [SerializeField] private bool addForce;
        [Sirenix.OdinInspector.EnableIf(nameof(addForce))]
        [SerializeField] private float force;
        
        [SerializeField] private int damage; // TODO: Make this more flexible
        
        [SerializeField] private LayerMask damageableLayer;
    
        private void OnTriggerEnter(Collider other)
        {
            if (damageableLayer == (damageableLayer | (1 << other.gameObject.layer)))
            {
                var damageable = other.GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                    
                    if (addForce)
                    {
                        var rb = other.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.AddForce((other.transform.position - transform.position).normalized * force, ForceMode.Impulse);
                        }
                    }
                }
            }
        }
    }
}
