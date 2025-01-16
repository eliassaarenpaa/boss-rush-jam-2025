using UnityEngine;
using Project.Runtime.Core.Input;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] TarotDataObject currentTarot;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] GameObject projectileBasePrefab;
    [SerializeField] ScriptableFloat playerHp;

    private float lastShootTime = 0f;
    private void Update()
    {
        //Check input
        if (PlayerInput.Attack)
        {
            if(1f/currentTarot.tarotBaseData.fireRate + lastShootTime < Time.time) //Fire rate is ok
            {
                lastShootTime = Time.time;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        //Spawn projectileBasePrefab.
        //Pass projectile data holder the data of the projectile
        for(int amountOfProjectiles = 0; amountOfProjectiles < currentTarot.tarotBaseData.projectileAmount; amountOfProjectiles++)
        {
            ProjectileAssembler pAssembler = Instantiate(projectileBasePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<ProjectileAssembler>();
            pAssembler.AssembleProjectile(currentTarot);
        }

    }
}