using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public static class EnterPlayMode 
    {
        [MenuItem("Enter Play Mode/Skip Intro #w")]
        private static void ToggleDebugGame()
        {
            EditorApplication.isPlaying = !Application.isPlaying;
        }
      
    }
}
