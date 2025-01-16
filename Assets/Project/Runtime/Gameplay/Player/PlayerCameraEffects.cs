using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Runtime.Gameplay.Player
{
    public class PlayerCameraEffects : MonoBehaviour
    {
        [SerializeField] private float fovRecoverySpeed = 1.0f;

        [SerializeField] private List<Camera> allCameras;
        [SerializeField] private Camera mainCamera;
        
        [ShowInInspector] [ReadOnly]
        private readonly Dictionary<Camera, float> _originalFOVs = new Dictionary<Camera, float>();
        [ShowInInspector] [ReadOnly]
        private Dictionary<Camera, float> _currentFOVs = new Dictionary<Camera, float>();

        private void Awake()
        {
            foreach (var cam in allCameras)
            {
                _originalFOVs.Add(cam, cam.fieldOfView);
            }
            
            _currentFOVs = new Dictionary<Camera, float>(_originalFOVs);
        }

        private void Update()
        {
            // FOV Animation
            foreach (var (cam, fov) in new Dictionary<Camera, float>(_currentFOVs))
            {
                // If the current FOV is greater than the original FOV
                var originalFOV = _originalFOVs[cam];
                if (fov > originalFOV)
                {
                    _currentFOVs[cam] -= Time.deltaTime * fovRecoverySpeed; // Decrease FOV

                    cam.fieldOfView = _currentFOVs[cam]; // Update FOV
                }
            }
        }

        public void PunchFOV(float amount)
        {
            foreach (var cam in allCameras)
            {
                _currentFOVs[cam] += amount;
            }
        }
    }
}
