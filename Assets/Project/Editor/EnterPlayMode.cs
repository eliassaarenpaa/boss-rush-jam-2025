using Project.Runtime.Core.Entry;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public static class EnterPlayMode 
    {
        [MenuItem("Enter Play Mode/Normal Boot #&e")]
        private static void ToggleNormalBoot()
        {
            Bootstrapper.config.isNormalBoot = true;
            ToggleBoot();
        }
        
        [MenuItem("Enter Play Mode/Debug Boot #&w")]
        private static void ToggleDebugBoot()
        {
            Bootstrapper.config.isNormalBoot = false;
            ToggleBoot();
        }

        private static void ToggleBoot()
        {
            // Dont enter play mode if the scene view is focused
            if (EditorWindow.focusedWindow == SceneView.lastActiveSceneView)
            {
                return;
            }
            
            EditorApplication.isPlaying = !Application.isPlaying;
        }
    }
}
