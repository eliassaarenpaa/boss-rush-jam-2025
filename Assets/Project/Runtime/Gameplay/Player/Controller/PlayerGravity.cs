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

            var x = Mathf.InverseLerp(0, maxGravityMagnitude, _force.magnitude);
            var y = gravityAccelCurve.Evaluate(x) * inAirAccelCurveMultiplier;

            _acceleration += Time.fixedDeltaTime * gravityAccelSpeed * y;
            _force += Vector3.down * _acceleration;
            _force = Vector3.ClampMagnitude(_force, maxGravityMagnitude);

            if (!GroundCheck.IsGrounded)
            {
                Rigidbody.AddForce(_force);
            }

            if (GroundCheck.IsGrounded)
            {
                SetAcceleration(0);
            }
        }

        public void SetAcceleration(float value)
        {
            _acceleration = value;
        }
    }
}
