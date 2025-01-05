using Project.Runtime.Core.Input;
using UnityEngine;

namespace Project.Runtime.Gameplay.Player.Controller
{
    public class PlayerCameraRotation : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float sensitivity = 0.1f;

        private Vector2 _mouseDeltaAxis = Vector2.zero;

        private void Update()
        {
            var viewDelta = PlayerInput.MouseDelta * sensitivity;

            _mouseDeltaAxis.x += viewDelta.x;
            _mouseDeltaAxis.y = Mathf.Clamp(_mouseDeltaAxis.y - viewDelta.y, -89, 89);

            camera.transform.rotation = Quaternion.AngleAxis(_mouseDeltaAxis.x, Vector3.up) * Quaternion.AngleAxis(_mouseDeltaAxis.y, Vector3.right);
        }
    }
}
