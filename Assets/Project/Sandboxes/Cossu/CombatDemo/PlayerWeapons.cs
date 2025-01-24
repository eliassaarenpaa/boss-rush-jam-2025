using Project.Runtime.Core.Input;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] PlayerInventoryScriptable playerInventory;
        WeaponData WeaponData;
        WeaponStatContainer weaponStats => WeaponData.baseStats;
        [SerializeField] Transform projectileSpawn;
        [SerializeField] GameObject projectileBasePrefab;
        [SerializeField] ScriptableFloat playerHp;

        private void Start()
        {
            UpdateWeaponData();
        }

        private void OnEnable()
        {
            playerInventory.OnInventoryChanged += UpdateWeaponData;
        }

        private void OnDisable()
        {
            playerInventory.OnInventoryChanged -= UpdateWeaponData;
        }

        private void UpdateWeaponData()
        {
            if(playerInventory.TryGetWeaponData(out WeaponData weaponData))
            {
                WeaponData = weaponData;
            }
        }

        private float lastShootTime = 0f;
        private void Update()
        {
            //Check input
            if (PlayerInput.Attack)
            {
                if (1f / WeaponData.baseStats.FireRate.TrueValue + lastShootTime < Time.time) //Fire rate is ok
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
            for (int amountOfProjectiles = 0; amountOfProjectiles < WeaponData.baseStats.ProjectileAmount.TrueValue; amountOfProjectiles++)
            {
                float projectileSpread = weaponStats.ProjectileSpread.TrueValue;
                Quaternion rotation;
                rotation = projectileSpawn.rotation;
                rotation.eulerAngles += new Vector3(Random.Range(-projectileSpread, projectileSpread), Random.Range(-projectileSpread, projectileSpread));
                ProjectileAssembler pAssembler = Instantiate(projectileBasePrefab, projectileSpawn.position, rotation).GetComponent<ProjectileAssembler>();
                if(playerInventory.TryGetWeaponData(out WeaponData weaponData))
                {
                    pAssembler.Assemble(weaponData); //Pass a new WeaponData instance to the projectile

                }
            }

        }
    }
}