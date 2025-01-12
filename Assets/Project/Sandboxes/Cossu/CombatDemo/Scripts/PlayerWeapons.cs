using UnityEngine;
using Project.Runtime.Core;
using Project.Runtime.Core.Input;

namespace Project.Sandboxes.Cossu.CombatDemo
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] WeaponScriptable weaponObject;
        [SerializeField] WeaponUtilityScriptable weaponUtilityCards;
        [SerializeField] Transform projectileSpawn;

        [SerializeField] private float lastAttackTime = 0f;
        private void Update()
        {
            if (PlayerInput.Attack && lastAttackTime + weaponObject.weaponBaseStats.weaponAttackSpeed <= Time.time)
            {
                //Attack code
                //
                // Play Anim -> Spawn Prefab -> Add component to the prefab based on weapon type -> Apply modifiers to the component
                //
                Attack();
                lastAttackTime = Time.time;
            }
        }

        private void Attack()
        {
            switch (weaponObject.weaponType)
            {
                case WeaponType.Kinematic:
                    //Spawn prefab -> Add kinematic projectile component -> Apply modifiers
                    GameObject projectileObject = Instantiate(weaponObject.gfxPrefab, projectileSpawn.position, projectileSpawn.transform.rotation);
                    KinematicProjectile kinematicProjectile = projectileObject.AddComponent<KinematicProjectile>();
                    kinematicProjectile.weaponBaseStats = weaponObject.weaponBaseStats;
                    kinematicProjectile.kinematicWeaponModifiers = weaponObject.kinematicWeaponModifiers;
                    break;
                case WeaponType.Physics:
                    //Spawn prefab -> Add physics projectile component -> Apply modifiers
                    break;
                case WeaponType.HitScan:
                    //Spawn prefab -> Add hit scan projectile component -> Apply modifiers
                    break;
            }
        }
    }
}
