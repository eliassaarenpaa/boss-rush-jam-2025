using Project.Runtime.Core.Extensions;
using Project.Runtime.Core.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Runtime.Gameplay.Player.Controller
{
    public class PlayerDash : PlayerComponent
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float dashHorizontalForce = 30.0f;
        [SerializeField] private float dashVerticalForce = 30.0f;
        [SerializeField] private int extraDashes;
        [SerializeField] private float dashRegenRate = 1.0f;
        [SerializeField] private float dashCooldown = 0.5f;

        [SerializeField] private float dashFOVAddon = 30.0f;
        private PlayerCameraEffects _cameraEffects;
        [ShowInInspector] [ReadOnly] private int _currentDashes;

        private float _lastDashTime;
        private float _lastRegenTime;

        private bool HasDashes => _currentDashes > 0;
        private static bool IsDashInput => PlayerInput.Dash;

        protected override void Awake()
        {
            base.Awake();
            _cameraEffects = GetComponent<PlayerCameraEffects>();
        }

        private void Start()
        {
            SetMaxDashes();
            _lastDashTime = Time.time;
            _lastRegenTime = Time.time;
        }

        private void Update()
        {
            if (IsDashInput && Time.time - _lastDashTime > dashCooldown)
            {
                if (HasDashes)
                {
                    _currentDashes--;
                    Dash();
                    _lastDashTime = Time.time;
                }
            }

            if (Time.time - _lastRegenTime > dashRegenRate)
            {
                AddDash();
                _lastRegenTime = Time.time;
            }
        }

        private void Dash()
        {
            StopRigidbody();
            AddForce();
            PunchFOV();
        }

        private void AddForce()
        {
            var moveInput = PlayerInput.Move;

            // The player is trying to strafe, so don't dash forward
            if (Mathf.Abs(moveInput.x) > 0.1f)
            {
                moveInput.y = 0;
            }

            var moveDir = camera.TransformToCameraSpace(moveInput);

            // Player is not moving, so default to dash forward
            if (moveDir.magnitude < 0.1f)
            {
                moveDir = camera.transform.forward;
            }

            moveDir.y = 0;

            var dashForce = moveDir * dashHorizontalForce + Vector3.up * dashVerticalForce;
            Rigidbody.AddForce(dashForce, ForceMode.Impulse);
        }

        private void StopRigidbody()
        {
            Rigidbody.linearVelocity = Vector3.zero;
            Gravity.SetAcceleration(0);
        }

        private void SetMaxDashes()
        {
            _currentDashes = 1 + extraDashes;
        }

        private void AddDash()
        {
            _currentDashes = Mathf.Min(1 + extraDashes, _currentDashes + 1);
        }

        private void PunchFOV()
        {
            _cameraEffects.PunchFOV(dashFOVAddon);
        }
    }
}
