using UnityEngine;

namespace Project.Runtime.Core.Input
{
    public static class Cursor
    {
        public static void Hide()
        {
            UnityEngine.Cursor.visible = false;
        }
        
        public static void Lock()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
