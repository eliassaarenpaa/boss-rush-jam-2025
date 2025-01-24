using Project.Runtime.Core.Input;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] PlayerInventoryScriptable playerInventory;
        WeaponData WeaponData;
        [SerializeField] Transform projectileSpawn;
        [SerializeField] GameObject projectileBasePrefab;
        [SerializeField] ScriptableFloat playerHp;

        private void Start()
        {
            WeaponData = playerInventory.TryGetWeaponData();
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
            WeaponData = playerInventory.TryGetWeaponData();
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
                ProjectileAssembler pAssembler = Instantiate(projectileBasePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<ProjectileAssembler>();
                pAssembler.Assemble(playerInventory.TryGetWeaponData()); //Pass a new WeaponData instance to the projectile
            }

        }
    }
}