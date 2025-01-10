using Animancer;
using UnityEngine;

namespace Project.z_Delete
{
    public class PlayerAnimationTest : MonoBehaviour
    {
        public AnimancerComponent anim;
        public ClipTransition clip;

        private void Start()
        {
            anim.Play(clip);
        }
    }
}
