using Project.Runtime.Core.Extensions;
using Project.Runtime.Core.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Runtime.Gameplay.Player.Controller
{
    public class PlayerMovement : PlayerComponent
    {
        [Header("Settings")]
        [SerializeField] private new Camera camera;

        [Tooltip("Acceleration rate while in the air.")]
        [SerializeField] private float airAccel = 10.0f;

        [Tooltip("Smoothing factor for air acceleration.")]
        [SerializeField] private float airAccelSmooth = 3.5f;

        [Tooltip("Maximum movement speed while in the air.")]
        [SerializeField] private float airMoveSpeed = 20.0f;

        [Tooltip("Acceleration rate while on the ground.")]
        [SerializeField] private float groundAccel = 10.0f;

        [Tooltip("Maximum movement speed while on the ground.")]
        [SerializeField] private float groundMoveSpeed = 15.0f;

        [Tooltip("Minimum speed required to start sliding.")]
        [SerializeField] private float minSlideSpeed = 7.0f;

        [Tooltip("Acceleration rate while sliding.")]
        [SerializeField] private float slideAccel = 1.0f;

        [Tooltip("Power factor applied to the velocity difference for movement calculation.")]
        [SerializeField] private float velocityPower = 1.2f;

        [Header("Debug")]
        [ShowInInspector] [ReadOnly] private Vector3 _moveDir;

        private void Update()
        {
            var moveInput = PlayerInput.Move;
            _moveDir = camera.TransformToCameraSpace(moveInput);
            _moveDir.y = 0;

            var a = Vector3.Cross(GroundCheck.GroundNormal, _moveDir).normalized;
            var groundTangent = -Vector3.Cross(GroundCheck.GroundNormal, a);
            groundTangent.Normalize();

            _moveDir = groundTangent;
        }

        private void FixedUpdate()
        {
            var accelRate = CalculateAccelerationRate();
            var speed = GroundCheck.IsGrounded ? groundMoveSpeed : airMoveSpeed;

            var targetVelocity = _moveDir * speed;
            var velocityDifference = targetVelocity - Rigidbody.linearVelocity;
            var movement = CalculateMovement(velocityDifference, accelRate);

            var slopeMultiplier = CalculateSlopeMultiplier(movement);

            Rigidbody.AddForce(movement * slopeMultiplier, ForceMode.Acceleration);
        }

        private float CalculateAccelerationRate()
        {
            if (GroundCheck.IsGrounded && Rigidbody.linearVelocity.magnitude <= groundMoveSpeed)
            {
                return PlayerInput.Crouching && Rigidbody.linearVelocity.magnitude > minSlideSpeed ? slideAccel : groundAccel;
            }

            return airAccel / Mathf.Max(Rigidbody.linearVelocity.magnitude, 1) * airAccelSmooth;
        }

        private Vector3 CalculateMovement(Vector3 velocityDifference, float accelRate)
        {
            return new Vector3(
                CalculateMovementOnAxis(velocityDifference.x, accelRate),
                CalculateMovementOnAxis(velocityDifference.y, accelRate),
                CalculateMovementOnAxis(velocityDifference.z, accelRate));
        }

        private float CalculateMovementOnAxis(float velocityDifference, float accelRate)
        {
            return Mathf.Pow(Mathf.Abs(velocityDifference) * accelRate, velocityPower) * Mathf.Sign(velocityDifference);
        }

        private static float CalculateSlopeMultiplier(Vector3 movement)
        {
            return 1 - Mathf.InverseLerp(-1, 1, Vector3.Dot(movement.normalized, Vector3.up));
        }
    }
}
