using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight
{
    public class MeatballProjectileShooter : MonoBehaviour
    {
        [SerializeField] private Collider shooterCollider;
        [SerializeField] private float projectileBurstCooldown;
        [SerializeField] private int burstCount;
        [SerializeField] private float burstInterval;

        [SerializeField] private List<Transform> projectileSpawnPoints;
        
        private float _lastBurstTime;
        
        private void Update()
        {
            if (Time.time - _lastBurstTime > projectileBurstCooldown)
            {
                StartCoroutine(BurstShootCoroutine());
                _lastBurstTime = Time.time;
            }
        }

        private Transform GetRandomSpawnPoint()
        {
            return projectileSpawnPoints[Random.Range(0, projectileSpawnPoints.Count)];
        }
        
        private IEnumerator BurstShootCoroutine()
        {
            for (int i = 0; i < burstCount; i++)
            {
                StartCoroutine(ShootProjectileCoroutine(GetRandomSpawnPoint()));
                yield return new WaitForSeconds(burstInterval);
            }
        }
        
        IEnumerator ShootProjectileCoroutine(Transform spawnPoint)
        {
            var prefab = (GameObject)Resources.Load("Meatball_Projectile");
            GameObject projectile = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

            var meatballProjectile = projectile.GetComponent<MeatballProjectile>();
            meatballProjectile.SetShooterCollider(shooterCollider);
            meatballProjectile.Move(spawnPoint.forward);
            
            yield return new WaitForSeconds( 100.0f );
            
            Destroy(projectile.gameObject);
        }
    }
}
