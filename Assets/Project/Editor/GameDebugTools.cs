using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public class GameDebugTools : OdinEditorWindow
    {
        [Title("Time Scale")]
        [ShowInInspector]
        public float TimeScale => Time.timeScale;
        
        [HorizontalGroup("TimeScale Buttons")]
        [Button(ButtonSizes.Large)]
        [MenuItem("Debugging Tools/SetTimeScale/Default &1")]
        public static void Normal()
        {
            Time.timeScale = 1;
        }
        [HorizontalGroup("TimeScale Buttons")]
        [Button(ButtonSizes.Large)]
        [MenuItem("Debugging Tools/SetTimeScale/Fast &2")]
        public static void Fast()
        {
            Time.timeScale = 10;
        }
        [HorizontalGroup("TimeScale Buttons")]
        [Button(ButtonSizes.Large)]
        [MenuItem("Debugging Tools/SetTimeScale/Slow &3")]
        public static void Slow()
        {
            Time.timeScale = 0.5f;
        }

        [Button]
        private void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }
        
        [MenuItem("Debugging Tools/Game Debug Tools Window")]
        public static void OpenWindow()
        {
            var window = GetWindow<GameDebugTools>("Game Debug Tools");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
        }
    }
}
