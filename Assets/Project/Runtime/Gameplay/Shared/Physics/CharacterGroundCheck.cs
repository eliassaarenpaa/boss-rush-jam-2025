using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Runtime.Gameplay.Shared.Physics
{
    public class CharacterGroundCheck : MonoBehaviour
    {
        [Header("Settings")]
        public LayerMask groundLayers;
        public float groundCheckOrigin = 0.5f;
        public float groundCheckDist = 0.5f;
        public float groundCheckSize = 0.35f;

        [Header("Debug")]
        [ShowInInspector] [ReadOnly] public bool IsGrounded { get; private set; }
        [ShowInInspector] [ReadOnly] public bool LastGroundTimeIsChecked { get; private set; }
        [ShowInInspector] [ReadOnly] public float LastGroundedTime { get;private set; }
        [ShowInInspector] [ReadOnly] public Vector3 GroundNormal { get; private set; }
        [ShowInInspector] [ReadOnly] public RaycastHit GroundHit { get; private set; }

        private Vector3 GroundCheckOrigin => transform.position + Vector3.up * groundCheckOrigin;

        private void FixedUpdate()
        {
            CheckForGround();
            UpdateLastGroundedTime();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GroundCheckOrigin + Vector3.down * groundCheckDist, groundCheckSize);
        }

        private void CheckForGround()
        {
            UnityEngine.Physics.SphereCast(GroundCheckOrigin, groundCheckSize, Vector3.down, out var hit, groundCheckDist, groundLayers);

            GroundHit = hit;
            GroundNormal = hit.collider ? hit.normal : Vector3.up;
            IsGrounded = hit.collider;
        }

        private void UpdateLastGroundedTime()
        {
            if (!IsGrounded && !LastGroundTimeIsChecked)
            {
                LastGroundTimeIsChecked = true;
                LastGroundedTime = Time.time;
            } else if (IsGrounded && LastGroundTimeIsChecked)
            {
                LastGroundTimeIsChecked = false;
                LastGroundedTime = 0;
            }
        }
        
        public void SetLastGroundedTime(float time)
        {
            LastGroundedTime = time;
        }
    }
}
