using UnityEngine;

namespace Project.Runtime.Gameplay.Player.Controller
{
    public class PlayerDrag : PlayerComponent
    {
        public float airDrag = 0.1f;
        public float groundDrag = 0.5f;
        public float angularDrag = 0.05f;
        public float dragTransitionSmooth;

        private void Start()
        {
            Rigidbody.linearDamping = airDrag;
            Rigidbody.angularDamping = angularDrag;
        }

        private void Update()
        {
            var targetDrag = GroundCheck.IsGrounded ? groundDrag : airDrag;

            Rigidbody.linearDamping = Mathf.Lerp(Rigidbody.linearDamping, targetDrag, dragTransitionSmooth * Time.deltaTime);
        }
    }
}
