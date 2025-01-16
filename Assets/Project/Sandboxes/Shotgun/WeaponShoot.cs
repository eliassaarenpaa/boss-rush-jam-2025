using Animancer;
using UnityEngine;

namespace Project.Sandboxes.Shotgun
{
    [RequireComponent(typeof(WeaponAmmo))]
    [RequireComponent(typeof(WeaponReload))]
    public class WeaponShoot : MonoBehaviour
    {
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

                if (hasAmmo && canFire && !isReloading)
                {
                    _timeSinceLastShot = Time.time;

                    PlayShootAnimation();

                    _ammo.Consume();
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
