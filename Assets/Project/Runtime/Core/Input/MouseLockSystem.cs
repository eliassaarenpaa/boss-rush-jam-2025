using UnityEngine;

namespace Project.Runtime.Core.Input
{
    public class MouseLockSystem : MonoBehaviour
    {
        private void Awake()
        {
            HideAndLockCursor();
        }

        private static void HideAndLockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
