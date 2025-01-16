using Animancer;
using UnityEngine;

namespace Project.Sandboxes.Shotgun
{
    public class ShotgunReload : MonoBehaviour
    {
        public AnimancerComponent anim;
        public ClipTransition shotgun_reload;

        private void Awake()
        {
            anim.Stop(shotgun_reload);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }

        private void Reload()
        {
            anim.Stop(shotgun_reload);
            anim.Play(shotgun_reload);

            // TODO: Add shotgun reload logic here
        }
    }
}
