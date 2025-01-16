using Animancer;
using UnityEngine;

namespace Project.Sandboxes.Shotgun
{
    public class ShotgunShoot : MonoBehaviour
    {
        public AnimancerComponent anim;
        public ClipTransition shotgun_shoot;
        [SerializeField] private float firerate = 0.5f;

        private float _timeSinceLastShot;

        private void Awake()
        {
            anim.Stop(shotgun_shoot);
            _timeSinceLastShot = Time.time;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - _timeSinceLastShot > firerate)
                {
                    _timeSinceLastShot = Time.time;
                    OnShoot();
                }
            }
        }

        private void OnShoot()
        {
            anim.Stop(shotgun_shoot);
            anim.Play(shotgun_shoot);

            // TODO: Add shotgun projectile shoot logic here
        }
    }
}
