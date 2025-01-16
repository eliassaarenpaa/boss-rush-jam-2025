using Project.Runtime.Core.Extensions;
using Project.Runtime.Core.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Runtime.Gameplay.Player.Controller
{
    public class PlayerJump : PlayerComponent
    {
        [Header("Settings")]
        [SerializeField] private new UnityEngine.Camera camera;
        [SerializeField] private int extraJumps;
        [SerializeField] private float jumpVerticalForce = 35.0f;
        [SerializeField] private float airJumpForwardForce = 15.0f;
        [SerializeField] private float jumpInputBuffer = 0.15f;
        [SerializeField] private float jumpCooldown = 0.025f;
        [SerializeField] private float maxJumpResetCooldown;
        [SerializeField] private float coyoteTime = 0.1f;

        [Header("Debug")]
        [ShowInInspector] [ReadOnly] private int _currentJumps;
        [ShowInInspector] [ReadOnly] private bool _isJumping;
        [ShowInInspector] [ReadOnly] private float _lastJumpInputTime;
        [ShowInInspector] [ReadOnly] private float _lastJumpStartTime;

        private bool IsJump => JumpIsNotOnCooldown && JumpIsBuffered && (GroundCheck.IsGrounded || !GroundCheck.IsGrounded && IsCoyoteTime || !GroundCheck.IsGrounded && _currentJumps > 0);
        private bool JumpIsBuffered => _lastJumpInputTime > 0 && Time.time - _lastJumpInputTime <= jumpInputBuffer;
        private bool IsCoyoteTime => GroundCheck.LastGroundedTime > 0 && Time.time - GroundCheck.LastGroundedTime <= coyoteTime;
        private bool JumpIsNotOnCooldown => _lastJumpStartTime <= 0 || Time.time - _lastJumpStartTime > jumpCooldown;

        private void Start()
        {
            SetMaxJumps();
        }

        private void Update()
        {
            if (PlayerInput.Jump) BufferJump();
            if (IsJump) Jump();
            if (GroundCheck.IsGrounded && !_isJumping) SetMaxJumps();

            if (_isJumping && GroundCheck.IsGrounded && _lastJumpStartTime > 0 && Time.time - _lastJumpStartTime > maxJumpResetCooldown)
            {
                _isJumping = false;
            }
        }

        private void BufferJump()
        {
            _lastJumpInputTime = Time.time;
        }

        private void Jump()
        {
            _isJumping = true;

            _currentJumps--;

            Gravity.SetAcceleration(0);
            _lastJumpStartTime = Time.time;
            GroundCheck.SetLastGroundedTime(0);
            _lastJumpInputTime = 0;

            var vel = Rigidbody.linearVelocity;
            var velocityMultiplier = 0.5f;
            vel.x *= velocityMultiplier;
            vel.y = 0;
            vel.z *= velocityMultiplier;
            Rigidbody.linearVelocity = vel;

            var moveInput = PlayerInput.Move;
            var moveDir = camera.TransformToCameraSpace(moveInput);
            moveDir.y = 0;

            var targetJumpVelocity = GroundCheck.IsGrounded
                ? Vector3.up * jumpVerticalForce
                : Vector3.up * jumpVerticalForce + moveDir * airJumpForwardForce;

            if (Rigidbody.linearVelocity.y < 0)
            {
                var jumpDifference = targetJumpVelocity.y - Rigidbody.linearVelocity.y;
                Debug.Log(jumpDifference);
                targetJumpVelocity = Vector3.up * jumpDifference;
            }

            Rigidbody.AddForce(targetJumpVelocity, ForceMode.Impulse);
        }

        private void SetMaxJumps()
        {
            _currentJumps = extraJumps;
        }
    }
}
