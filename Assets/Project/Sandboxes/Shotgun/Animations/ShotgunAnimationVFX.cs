using UnityEngine;

namespace Project.Sandboxes.Shotgun.Animations
{
    public class ShotgunAnimationVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private ParticleSystem _barrelSmoke;
        [SerializeField] private ParticleSystem _shellEject;

        private void OnDisable()
        {
            _muzzleFlash.Stop();
            _barrelSmoke.Stop();
        }

        /// <summary>
        ///     Called from animation event
        /// </summary>
        public void PlayMuzzleFlash()
        {
            _muzzleFlash.Play();
        }

        /// <summary>
        ///     Called from animation event
        /// </summary>
        public void PlayBarrelSmoke()
        {
            _barrelSmoke.Play();
        }

        /// <summary>
        ///     Called from animation event
        /// </summary>
        public void PlayShellEject()
        {
            _shellEject.Play();
        }
    }
}
