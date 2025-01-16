using Animancer;
using UnityEngine;

namespace Project.Sandboxes.Shotgun
{
    [RequireComponent(typeof(WeaponAmmo))]
    public class WeaponReload : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent anim;
        [SerializeField] private  ClipTransition reloadAnimationClip;
        
        private WeaponAmmo _ammo;

        public bool IsReloading => anim.IsPlaying(reloadAnimationClip) && !_ammo.IsMax;

        private void Awake()
        {
            _ammo = GetComponent<WeaponAmmo>();
            anim.Stop(reloadAnimationClip);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (IsReloading || _ammo.IsMax)
                {
                    return;
                }

                PlayReloadAnimation();
            }
        }

        public void PlayReloadAnimation()
        {
            anim.Stop();
            anim.Play(reloadAnimationClip);
        }

        /// <summary>
        ///     Called from animation event
        /// </summary>
        public void PlayReloadSound()
        {
            // TODO
        }
    }
}
