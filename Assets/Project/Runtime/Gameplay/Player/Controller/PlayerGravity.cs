using Project.Runtime.Core.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Runtime.Gameplay.Player.Controller
{
    public class PlayerGravity : PlayerComponent
    {
        [Header("Settings")]
        public float gravityAccelSpeed = 15.0f;
        public float maxGravityMagnitude = 100.0f;
        [Tooltip("x = approaching max gravity, y = gravity multiplier")]
        public AnimationCurve gravityAccelCurve;
        public float inAirAccelCurveMultiplier = 10.0f;
        public float slopeAccelCurveMultiplier;
        public float slopeAngle = 35.0f;
        public float slopeSlideAccelSpeed = 50.0f;
        [ShowInInspector] [ReadOnly] private float _acceleration;

        [Header("Debug")]
        [ShowInInspector] [ReadOnly] private Vector3 _force;

        private void FixedUpdate()
        {
            var isSlideOnNonSteepSurface = IsSlideOnNonSteepSurface();

            var x = Mathf.InverseLerp(0, maxGravityMagnitude, _force.magnitude);
            var y = gravityAccelCurve.Evaluate(x) * (!isSlideOnNonSteepSurface ? inAirAccelCurveMultiplier : slopeAccelCurveMultiplier);

            _acceleration += Time.fixedDeltaTime * (!isSlideOnNonSteepSurface ? gravityAccelSpeed : slopeSlideAccelSpeed) * y;
            _force += Vector3.down * _acceleration;
            _force = Vector3.ClampMagnitude(_force, maxGravityMagnitude);

            if (!GroundCheck.IsGrounded || GroundIsSlope() || isSlideOnNonSteepSurface)
            {
                Rigidbody.AddForce(_force);
            }

            if (GroundCheck.IsGrounded && !IsSlideOnNonSteepSurface())
            {
                SetAcceleration(0);
            }
        }

        private bool GroundIsSlope()
        {
            if (!GroundCheck.IsGrounded) return false;
            var groundNormalAngle = Vector3.Angle(GroundCheck.GroundNormal, Vector3.up);
            return groundNormalAngle >= slopeAngle;
        }

        private bool IsSlideOnNonSteepSurface()
        {
            if (GroundCheck.GroundNormal == Vector3.up) return false;

            var isSlidingOnSlope = GroundCheck.IsGrounded && !GroundIsSlope() && PlayerInput.Crouching;
            return isSlidingOnSlope;
        }

        public void SetAcceleration(float value)
        {
            _acceleration = value;
        }
    }
}
