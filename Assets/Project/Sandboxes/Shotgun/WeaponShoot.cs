using Animancer;
using Project.Runtime.Core.Input;
using Project.Runtime.Gameplay.Player.Controller;
using UnityEngine;

namespace Project.Sandboxes.Shotgun
{
    [RequireComponent(typeof(WeaponAmmo))]
    [RequireComponent(typeof(WeaponReload))]
    public class WeaponShoot : MonoBehaviour
    {
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
                        if (Vector3.Dot(mainCamera.transform.forward, Vector3.down) >= shotgunJumpDot )
                        {
                            // var vel = rb.linearVelocity;
                            // vel.y = 0;
                            // rb.linearVelocity = vel;
                            gravity.SetAcceleration(0);
                        
                            rb.AddForce(Vector3.up * shotgunJumpForce, ForceMode.Impulse);
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
