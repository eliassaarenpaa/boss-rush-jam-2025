using UnityEngine;

namespace Project.Runtime.Core.Input
{
    public static class PlayerInput
    {
        public static Vector2 MouseDelta => new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
        public static Vector3 Move => new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), 0, UnityEngine.Input.GetAxisRaw("Vertical"));
        public static bool IsHoldingMoveDirection => Move.magnitude > 0;
        public static bool Crouching => UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.C);
        public static bool Jump => UnityEngine.Input.GetKeyDown(KeyCode.Space);
        public static bool JumpHeld => UnityEngine.Input.GetKey(KeyCode.Space);
        public static bool Attack => UnityEngine.Input.GetMouseButtonDown(0);
        public static bool Dash => UnityEngine.Input.GetKeyDown(KeyCode.LeftShift);
    }
}
