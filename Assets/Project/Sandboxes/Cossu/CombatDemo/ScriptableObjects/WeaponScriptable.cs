using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Sandboxes.Cossu.CombatDemo
{
    [System.Serializable]
    public class KinematicWeaponModifiers
    {
        public float kinematicProjectileSpeed;
        public int kinematicProjectileRicochetAmount;
        public int kinematicProjectilePierceAmount;
    }

    [System.Serializable]
    public class PhysicsWeaponModifiers
    {
        public float physicsProjectileDrag;
        public float physicsProjectileSpeed;
    }

    [System.Serializable]
    public class HitScanWeaponModifiers
    {
        
    }

    [System.Serializable]
    public class WeaponBaseStats
    {
        public float weaponDamage;
        public float weaponAttackSpeed;
    }
    public enum WeaponType
    {
        Kinematic,
        Physics,
        HitScan
    }

    [CreateAssetMenu(fileName = "WeaponObject", menuName = "SO/WeaponObject", order = 0)]
    public class WeaponScriptable : ScriptableObject
    {
        public WeaponType weaponType;

        [ShowIf("weaponType", WeaponType.Kinematic)]
        [Title("Kinematic Projectile")]
        public KinematicWeaponModifiers kinematicWeaponModifiers;

        [ShowIf("weaponType", WeaponType.Physics)]
        [Title("Physics Projectile")]
        public PhysicsWeaponModifiers physicsWeaponModifiers;

        [ShowIf("weaponType", WeaponType.HitScan)]
        [Title("Hit Scan")]
        public HitScanWeaponModifiers hitScanWeaponModifiers;

        #region General Properties
        [Title("General Modifiers")]
        public WeaponBaseStats weaponBaseStats;
        public GameObject gfxPrefab;
        public AnimationClip attackAnim;
        #endregion
    }
}