using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ScriptBoy.MotionPathAnimEditor
{
    partial class AnimEditorWindow
    {
        class MotionPathListRenderer : ReorderableList
        {
            List<MotionPath> m_List;

            public MotionPathListRenderer(List<MotionPath> list) : base(list, typeof(MotionPath))
            {
                m_List = list;
                headerHeight = 5;
                elementHeight = 24;
                onAddCallback += OnAddCallback;
                onCanAddCallback += OnCanAddCallback;
                drawHeaderCallback += DrawHeaderCallback;
                drawElementCallback += DrawElementCallback;
                drawElementBackgroundCallback += DrawElementBackgroundCallback;
            }

            private void DrawElementBackgroundCallback(Rect rect, int index, bool isActive, bool isFocused)
            {
                GUI.DrawTexture(rect, isActive ? Textures.itemRowActive : Textures.itemRowNormal);
            }

            private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
            {
                var motionPath = m_List[index];
                GUI.Label(rect, Settings.showPathFullName ? motionPath.fullName : motionPath.name);

                float s = rect.height - 2;
                rect = new Rect(rect.x + rect.width - s * 3, rect.y, s, s);

                if (Settings.showPathEditButton)
                {
                    if (motionPath.HasCurveData) MotionPathToggleButtons.loop.Draw(rect, m_List, index);
                    rect.x += s;
                    if (motionPath.HasCurveData) MotionPathToggleButtons.edit.Draw(rect, m_List, index);
                    rect.x += s;
                }
                else
                {
                    rect.x += s;
                    if (motionPath.HasCurveData) MotionPathToggleButtons.loop.Draw(rect, m_List, index);
                    rect.x += s;
                }

                MotionPathToggleButtons.active.Draw(rect, m_List, index);
            }

            private void DrawHeaderCallback(Rect rect)
            {

            }

            private bool OnCanAddCallback(ReorderableList list)
            {
                foreach (var transform in Selection.transforms)
                {
                    if (CanBeAdded(transform))
                    {
                        return true;
                    }
                }
                return false;
            }

            private void OnAddCallback(ReorderableList list)
            {
                foreach (var transform in Selection.GetFiltered<Transform>(SelectionMode.ExcludePrefab))
                {
                    if (CanBeAdded(transform))
                    {
                        m_List.Add(new MotionPath(transform));
                    }
                }
            }

            bool CanBeAdded(Transform transform)
            {
                if (m_List.Exists(e => e.transform == transform)) return false;
                Transform root = AnimEditor.root;
                if(transform == root) return true;
                if (transform.IsChildOf(root)) return true;
                return false;
            }
        }

        static class MotionPathToggleButtons
        {
            public static MotionPathToggleButton active { get; }
            public static MotionPathToggleButton edit { get; }
            public static MotionPathToggleButton loop { get; }

            static MotionPathToggleButtons()
            {
                active =  new MotionPathToggleButton(GUIContents.activePath, GUIStyles.toggleVisibility, (e) => e.active, (e, v) => e.active = v);
                edit = new MotionPathToggleButton(GUIContents.editPath, GUIStyles.toggleEdit, (e) => e.editable, (e, v) => e.editable = v);
                loop = new MotionPathToggleButton(GUIContents.loopPath, GUIStyles.toggleLoop, (e) => e.loop, (e, v) => e.loop = v);
            }
        }

        class MotionPathToggleButton
        {
            GUIContent m_Content;
            GUIStyle m_Style;
            Func<MotionPath, bool> m_GetValue;
            Action<MotionPath, bool> m_SetValue;

            public MotionPathToggleButton(GUIContent content, GUIStyle style, Func<MotionPath, bool> getValue, Action<MotionPath, bool> setValue)
            {
                m_Content = content;
                m_Style = style;
                m_GetValue = getValue;
                m_SetValue = setValue;
            }

            public void Draw(Rect rect, List<MotionPath> list, int index)
            {
                MotionPath motionPath = list[index];

                bool value = m_GetValue(motionPath);
                rect = ApplyMargin(rect, 4);
                int button = Event.current.button;
                EditorGUI.BeginChangeCheck();
                value = GUI.Toggle(rect, value, m_Content, m_Style);
                if (EditorGUI.EndChangeCheck())
                {
                    if (button == 1)
                    {
                        bool oldValue = m_GetValue(motionPath);
                        m_SetValue(motionPath, false);
                        bool all = oldValue && list.TrueForAll((e) => !m_GetValue(e));
                        foreach (var e in list) m_SetValue(e, all);
                        value = true;
                    }
                    m_SetValue(motionPath, value);
                }
            }

            Rect ApplyMargin(Rect rect, float margin)
            {
                return new Rect(rect.x + margin, rect.y + margin, rect.width - margin, rect.height - margin);
            }
        }
    }
}