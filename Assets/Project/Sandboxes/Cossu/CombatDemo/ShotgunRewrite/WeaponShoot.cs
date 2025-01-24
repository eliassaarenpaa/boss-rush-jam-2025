using Animancer;
using Project.Runtime.Core.Input;
using Project.Runtime.Gameplay.Player.Controller;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    [RequireComponent(typeof(WeaponAmmo))]
    [RequireComponent(typeof(WeaponReload))]
    public class WeaponShoot : MonoBehaviour
    {
        [SerializeField] PlayerInventoryScriptable playerInventory;
        [SerializeField] Transform projectileSpawn;
        [SerializeField] GameObject projectileBasePrefab;
        [SerializeField] private bool useDownwardFacingJump;
        [SerializeField] private PlayerGravity gravity;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float shotgunJumpDot;
        [SerializeField] private float shotgunJumpForce;
        private WeaponAmmo _ammo;
        private WeaponReload _reload;

        [SerializeField] private float firerate = 0.5f;
        private float _timeSinceLastShot;

        [SerializeField] private AnimancerComponent anim;
        [SerializeField] private ClipTransition shootAnimationClip;

        private void Awake()
        {
            _ammo = GetComponent<WeaponAmmo>();
            _reload = GetComponent<WeaponReload>();
            anim.Stop(shootAnimationClip);
            _timeSinceLastShot = Time.time;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var isReloading = _reload.IsReloading;
                var hasAmmo = _ammo.HasAmmo;
                var canFire = Time.time - _timeSinceLastShot > firerate;

                if (!hasAmmo && !_reload.IsReloading)
                {
                    // Quality of life: if no ammo, but trying to shoot, reload
                    _reload.PlayReloadAnimation();
                }
                else if (canFire && !isReloading)
                {
                    _timeSinceLastShot = Time.time;

                    PlayShootAnimation();

                    _ammo.Consume();

                    if (useDownwardFacingJump)
                    {
                        // Jump if facing down
                        if (Vector3.Dot(mainCamera.transform.forward, Vector3.down) >= shotgunJumpDot)
                        {
                            // var vel = rb.linearVelocity;
                            // vel.y = 0;
                            // rb.linearVelocity = vel;
                            gravity.SetAcceleration(0);

                            rb.AddForce(Vector3.up * shotgunJumpForce, ForceMode.Impulse);
                        }
                    }

                    if(playerInventory.TryGetWeaponData(out WeaponData WeaponData))
                    {
                        WeaponStatContainer weaponStats = WeaponData.baseStats;
                        //Shooting logic
                        //Spawn projectileBasePrefab.
                        //Pass projectile data holder the data of the projectile
                        for (int amountOfProjectiles = 0; amountOfProjectiles < WeaponData.baseStats.ProjectileAmount.TrueValue; amountOfProjectiles++)
                        {
                            float projectileSpread = weaponStats.ProjectileSpread.TrueValue;
                            Quaternion rotation;
                            rotation = projectileSpawn.rotation;
                            rotation.eulerAngles += new Vector3(Random.Range(-projectileSpread, projectileSpread), Random.Range(-projectileSpread, projectileSpread));
                            ProjectileAssembler pAssembler = Instantiate(projectileBasePrefab, projectileSpawn.position, rotation).GetComponent<ProjectileAssembler>();
                            if (playerInventory.TryGetWeaponData(out WeaponData weaponData))
                            {
                                pAssembler.Assemble(weaponData); //Pass a new WeaponData instance to the projectile

                            }
                        }
                    }
                }
            }
        }

        private void PlayShootAnimation()
        {
            anim.Stop();
            anim.Play(shootAnimationClip);
        }

        /// <summary>
        ///     Called from animation event
        /// </summary>
        public void PlayShootSound()
        {
            // TODO
        }
    }
}
