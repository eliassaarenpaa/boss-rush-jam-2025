using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public static class InspectorLockToggle
    {
        private static EditorWindow _mouseOverWindow;

        [MenuItem("Inspector/Select Under Mouse Cursor (Use Hotkey) #&q")]
        public static void Select()
        {
            _mouseOverWindow = EditorWindow.mouseOverWindow;

            
        }

        [MenuItem("Inspector/Toggle Lock &q")]
        private static void ToggleInspectorLock()
        {
            if (_mouseOverWindow == null)
            {
                if (!EditorPrefs.HasKey("LockableInspectorIndex"))
                    EditorPrefs.SetInt("LockableInspectorIndex", 0);
                var i = EditorPrefs.GetInt("LockableInspectorIndex");

                var type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.InspectorWindow");
                var findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
                _mouseOverWindow = (EditorWindow)findObjectsOfTypeAll[i];
            }

            if (_mouseOverWindow != null && _mouseOverWindow.GetType().Name == "InspectorWindow")
            {
                var type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.InspectorWindow");
                var propertyInfo = type.GetProperty("isLocked");
                var value = (bool)propertyInfo.GetValue(_mouseOverWindow, null);
                propertyInfo.SetValue(_mouseOverWindow, !value, null);
                _mouseOverWindow.Repaint();
            }
        }
    }
}