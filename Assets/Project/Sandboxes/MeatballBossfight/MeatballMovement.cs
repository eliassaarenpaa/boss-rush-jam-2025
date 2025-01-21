using System;
using Project.Sandboxes.ScriptableValues;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Sandboxes.MeatballBossfight
{
    public class MeatballMovement : MonoBehaviour
    {
        [SerializeField] private Damageable damageable;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float movementSpeedMin;
        [SerializeField] private float movementSpeedMax;
        
        private float _movementSpeed;
        private float _lastDirectionChangeTime;
        private Vector3 _movementDirection;

        private void Start()
        {
            var randDir = GetRandomDirection();
            _movementDirection = randDir;
            RotateTowardsDirection(randDir);
            SetMovementSpeed(damageable.Health);
        }

        private void OnEnable()
        {
            damageable.Health.OnValueChanged += SetMovementSpeed;
        }
        
        private void OnDisable()
        {
            damageable.Health.OnValueChanged -= SetMovementSpeed;
        }
        
        void SetMovementSpeed(OperationResult<int> result)
        {
            SetMovementSpeed(damageable.Health);
        }

        private void FixedUpdate()
        {
            MoveTowardsMovementDirection();
            RotateTowardsMovementDirection();
        }
        
        private void MoveTowardsMovementDirection()
        {
            rb.linearVelocity = _movementDirection * _movementSpeed;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
            {
                _movementDirection = SetNewMovementDirection(Vector3.Reflect(_movementDirection, other.contacts[0].normal));
            }
        }
        
        private Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        
        private void RotateTowardsDirection(Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _movementSpeed * Time.fixedDeltaTime);
        }
        
        private void RotateTowardsMovementDirection()
        {
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                var targetRotation = Quaternion.LookRotation(rb.linearVelocity);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _movementSpeed * Time.fixedDeltaTime);
            }
        }
        
        private Vector3 SetNewMovementDirection( Vector3 direction )
        {
            Vector3 tmp = direction;

            // Make sure the new direction is not too close to the current direction
            while (Vector3.Dot( tmp, _movementDirection ) > -0.2f)
            {
                tmp = GetRandomDirection();
            }
            
            return tmp;
        }
        
        public void SetMovementSpeed(IntValue health)
        {
            var movementLerpT = 1 - Mathf.InverseLerp(health.GetMinValue(), health.GetMaxValue(), health.Value);
            _movementSpeed = Mathf.Lerp(movementSpeedMin, movementSpeedMax, movementLerpT);
        }
    }
}
