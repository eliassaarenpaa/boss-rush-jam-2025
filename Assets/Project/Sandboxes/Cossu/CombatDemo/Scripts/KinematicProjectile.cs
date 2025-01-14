using System.ComponentModel;
using UnityEngine;

namespace Project.Sandboxes.Cossu.CombatDemo
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicProjectile : MonoBehaviour
    {
        public KinematicWeaponModifiers kinematicWeaponModifiers;
        public WeaponBaseStats weaponBaseStats;

        [SerializeField] private int ricochetAmount;
        [SerializeField] private int pierceAmount;

        private void Start()
        {
            //Make sure this object is kinematic
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            GetComponent<BoxCollider>().isTrigger = true;

            //Set ricochet and pierce amounts
            ricochetAmount = kinematicWeaponModifiers.kinematicProjectileRicochetAmount;
            pierceAmount = kinematicWeaponModifiers.kinematicProjectilePierceAmount;
        }

        private void Update()
        {
            MoveProjectile();
        }

        public void MoveProjectile()
        {
            transform.position = transform.position + transform.forward * kinematicWeaponModifiers.kinematicProjectileSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") return;
            if(other.TryGetComponent<Health>(out Health health))
            {
                DealDamage(health);
                if (pierceAmount > 0) //Handle piercing here
                {
                    pierceAmount -= 1;
                }
                else
                {
                    pierceAmount = 0;
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject); //Ricochet not implemented yet.
            }
        }

        private void DealDamage(Health targetHealth)
        {
            targetHealth.ChangeCurrentHealth(weaponBaseStats.weaponDamage, ScriptableFloat.FloatChangeType.Decrease);

        }
    }
}

