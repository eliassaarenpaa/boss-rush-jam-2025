using UnityEngine;
using Project.Runtime.Core.Input;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] TarotDataObject currentTarot;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] GameObject projectileBasePrefab;

    private float lastShootTime = 0f;
    private void Update()
    {
        //Check input
        if (PlayerInput.Attack)
        {
            if(currentTarot.tarotBaseData.fireRate + lastShootTime < Time.time) //Fire rate is ok
            {
                lastShootTime = Time.time;
            }
            else
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        //Spawn projectileBasePrefab.
        //Pass projectile data holder the data of the projectile
        ProjectileDataHolder pDataHolder = Instantiate(projectileBasePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<ProjectileDataHolder>();
        pDataHolder.tarotDataObject = currentTarot;
    }
}