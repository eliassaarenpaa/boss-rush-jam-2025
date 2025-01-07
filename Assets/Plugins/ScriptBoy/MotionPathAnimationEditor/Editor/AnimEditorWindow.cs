using UnityEditor;
using UnityEngine;

namespace ScriptBoy.MotionPathAnimEditor
{
    partial class AnimEditorWindow : EditorWindow
    {
        private static AnimEditorWindow s_Instance;

        [SerializeField] private Material m_CurveMaterial;

        private AnimEditor m_AnimEditor;
        private HandleSelectionTransformEditor m_SelectionTransformEditor;
        private RootOffsetEditor m_RootOffsetEditor;
        private MotionPathListRenderer m_ListRenderer;

        private bool m_ShowFullName;
        private bool m_ShowSettings;
        private bool m_EditMode;

        [MenuItem("Tools/ScriptBoy/Motion Path Animation Editor", false, 0)]
        static void OpenWindow()
        {
            GetWindow<AnimEditorWindow>().Show();
        }

        public static void RepaintWindow()
        {
            if (s_Instance != null) s_Instance.Repaint();
        }

        private void Awake()
        {
            titleContent = new GUIContent("Motion Path Anim Editor");
            minSize = new Vector2(300, 200);
        }

        private void OnEnable()
        {
            s_Instance = this;
            BezierCurveRenderer.SetMaterial(m_CurveMaterial);
            m_SelectionTransformEditor = (HandleSelectionTransformEditor)Editor.CreateEditor(HandleSelectionTransform.instance);
            m_RootOffsetEditor = (RootOffsetEditor)Editor.CreateEditor(RootOffset.instance);
            if (m_AnimEditor != null) m_AnimEditor.Destroy();
            m_AnimEditor = new AnimEditor();
            m_ListRenderer = new MotionPathListRenderer(m_AnimEditor.motionPaths);
            m_AnimEditor.editMode = m_EditMode;
        }

        private void OnDisable()
        {
            DestroyImmediate(m_SelectionTransformEditor);
            DestroyImmediate(m_RootOffsetEditor);
            m_AnimEditor.Destroy();
            m_AnimEditor = null;
            Tools.hidden = false;
        }

        private void OnGUI()
        {
            //bool wideMode = EditorGUIUtility.wideMode;
            //EditorGUIUtility.wideMode = true;

            if (OnCheckStateGUI())
            {
                Tools.hidden = false;
                return;
            }

            Tools.hidden = m_EditMode;

            OnHeaderGUI();
            OnBodyGUI();

            //EditorGUIUtility.wideMode = wideMode;
        }

        private bool OnCheckStateGUI()
        {
            if (!AnimEditor.animationWindow)
            {
                AnimEditor.animationWindow = AnimationWindowWrapper.FindWindow();
            }

            if (!AnimEditor.animationWindow)
            {
                EditorGUILayout.HelpBox("No Animation Window!", MessageType.Error);
                if (GUILayout.Button("Open Animation Window"))
                {
                    AnimEditor.animationWindow = AnimationWindowWrapper.GetWindow();
                }
                return true;
            }

            if (AnimEditor.animationWindow.animationClip == null)
            {
                if (AnimEditor.animationWindow.hasFocus)
                {
                    EditorGUILayout.HelpBox("No Animation Clip!", MessageType.Error);
                }
                else
                {
                    EditorGUILayout.HelpBox("No Animation Window!", MessageType.Warning);
                    if (GUILayout.Button("Open Animation Window"))
                    {
                        AnimEditor.animationWindow.Focus();
                    }
                }
                return true;
            }

            if (AnimEditor.animationWindow.rootGameObject == null)
            {
                EditorGUILayout.HelpBox("No Root GameObject!", MessageType.Error);
                return true;
            }

            return false;
        }

        private void OnHeaderGUI()
        {
            using (new GUILayout.HorizontalScope(GUIStyles.header))
            {
                m_EditMode = GUILayout.Toggle(m_EditMode, GUIContents.editMode, GUIStyles.toggleEditMode);
                GUILayout.FlexibleSpace();
                m_ShowSettings = GUILayout.Toggle(m_ShowSettings, GUIContents.showSettings, GUIStyles.toggleSettings);

                m_AnimEditor.editMode = m_EditMode;
            }
        }

        private void OnBodyGUI()
        {
            if (m_ShowSettings)
            {
                Settings.DrawFoldoutWindow();
            }

            if (Settings.useRootOffset)
            {
                OnRootOffsetGUI();
            }

            if (Settings.useHideHandles)
            {
                HideHandles.DrawFoldoutWindow();
            }

            if (Settings.useTimeRange)
            {
                TimeRange.DrawFoldoutWindow();
            }

            if (Settings.useMagnet)
            {
                Magnet.DrawFoldoutWindow();
            }

            if (m_EditMode)
            {
                if (HandleSelection.count == 1)
                {
                    OnHandleGUI();
                }
                else if (HandleSelection.count > 1)
                {
                    OnSelectionTransformGUI();
                }
            }

            m_ListRenderer.DoLayoutList();
        }

        private void OnRootOffsetGUI()
        {
            EditorGUI.BeginChangeCheck();
            m_RootOffsetEditor.OnGUI();
            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
        }

        private void OnSelectionTransformGUI()
        {
            EditorGUI.BeginChangeCheck();
            m_SelectionTransformEditor.OnGUI();
            if (EditorGUI.EndChangeCheck())
            {
                HandleSelectionTransform.instance.UpdatePositions();
                m_AnimEditor.ApplyChages();
            }
        }

        private void OnHandleGUI()
        {
            EditorGUI.BeginChangeCheck();
            using (new CustomGUILayout.FoldoutWindowScope("Handle", out bool open))
            {
                if (open)
                {
                    HandleSelection.activeHandle.position = EditorGUILayout.Vector3Field("Position", HandleSelection.activeHandle.position);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                HandleSelection.activeHandle.hasChanged = true;
                m_AnimEditor.ApplyChages();
            }
        }
    }

    static class HideHandles
    {
        static bool s_Controls;
        static bool s_Tangents;

        public static bool controls => s_Controls && Settings.useHideHandles;
        public static bool tangents => s_Tangents && Settings.useHideHandles;

        public static void DrawFoldoutWindow()
        {
            EditorGUI.BeginChangeCheck();
            using (new CustomGUILayout.FoldoutWindowScope("Hide Handles", out bool open))
            {
                if (open)
                {
                    EditorGUILayout.BeginHorizontal();
                    s_Controls = CustomGUILayout.ToggleButton("Hide Controls", s_Controls);
                    s_Tangents = CustomGUILayout.ToggleButton("Hide Tangents", s_Tangents);
                    EditorGUILayout.EndHorizontal();
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
        }
    }

    static class Magnet
    {
        static float s_Radius = 5;
        static bool s_Controls = true;
        static bool s_Tangents = true;

        public static float radius => s_Radius;
        public static bool controls => s_Controls;
        public static bool tangents => s_Tangents;

        public static void DrawFoldoutWindow()
        {
            EditorGUI.BeginChangeCheck();
            using (new CustomGUILayout.FoldoutWindowScope("Magnet", out bool open))
            {
                if (open)
                {
                    GUILayout.BeginHorizontal();
                    s_Controls = CustomGUILayout.ToggleButton("Drag Controls", s_Controls);
                    s_Tangents = CustomGUILayout.ToggleButton("Drag Tangents", s_Tangents);
                    GUILayout.EndHorizontal();

                    s_Radius = EditorGUILayout.FloatField("Radius", s_Radius);
                    s_Radius = Mathf.Max(s_Radius, 0);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
        }
    }

    static class TimeRange
    {
        static float from = 0;
        static float to = 1;

        public static bool Contains(float time)
        {
            time /= AnimEditor.animationClip.length;
            return time >= from && time <= to;
        }

        public static void DrawFoldoutWindow()
        {
            EditorGUI.BeginChangeCheck();
            using (new CustomGUILayout.FoldoutWindowScope("Time Range", out bool open))
            {
                if (open)
                {
                    EditorGUILayout.MinMaxSlider(ref from, ref to, 0, 1);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
        }

        public static void SyncWithCurrentTime()
        {
            float prevFrom = from;
            float prevTo = to;
            float time = Mathf.Clamp01(AnimEditor.animationWindow.time / AnimEditor.animationClip.length);
            float delta = to - from;
            from = time - delta / 2;
            to = time + delta / 2;
            if (from < 0)
            {
                to -= from;
                from = 0;
            }

            if (to > 1)
            {
                from -= to - 1;
                to = 1;
            }

            if(from != prevFrom || to != prevTo) AnimEditorWindow.RepaintWindow();
        }
    }

}