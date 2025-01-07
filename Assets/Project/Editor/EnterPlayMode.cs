using Project.Runtime.Core.Entry;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public static class EnterPlayMode 
    {
        [MenuItem("Enter Play Mode/Normal Boot #e")]
        private static void ToggleNormalBoot()
        {
            Bootstrapper.config.isNormalBoot = true;
            EditorApplication.isPlaying = !Application.isPlaying;
        }
        
        [MenuItem("Enter Play Mode/Debug Boot #w")]
        private static void ToggleDebugBoot()
        {
            Bootstrapper.config.isNormalBoot = false;
            EditorApplication.isPlaying = !Application.isPlaying;
        }
    }
}
