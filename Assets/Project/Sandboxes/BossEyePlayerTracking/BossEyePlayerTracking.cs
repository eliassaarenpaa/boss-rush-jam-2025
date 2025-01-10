using UnityEngine;

namespace Project.Sandboxes.BossEyePlayerTracking
{
    public class BossEyePlayerTracking : MonoBehaviour
    {
        [SerializeField] private float _maxDistanceFromOrigin;
        [SerializeField] private float _movementSmooth;
    
        private Transform _playerTransform;
        private Vector3 _origin;
    
        void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            _origin = transform.position;
        }

        void Update()
        {
            if (_playerTransform == null)
            {
                _playerTransform = GameObject.FindWithTag("Player").transform;
                return;
            }
        
        
            Vector3 playerPosition = _playerTransform.position;
            Vector3 direction = playerPosition - _origin;
            float distance = direction.magnitude;
            if (distance > _maxDistanceFromOrigin)
            {
                direction = direction.normalized * _maxDistanceFromOrigin;
                playerPosition = _origin + direction;
            }
        
            transform.position = Vector3.Lerp(transform.position, playerPosition, _movementSmooth * Time.deltaTime);
        }
    }
}
