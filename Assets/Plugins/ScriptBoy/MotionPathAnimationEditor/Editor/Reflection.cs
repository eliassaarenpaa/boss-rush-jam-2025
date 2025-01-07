using System;
using UnityEngine;
using System.Reflection;

namespace ScriptBoy.MotionPathAnimEditor
{
    static class Reflection
    {
        static BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.GetField;

        [UnityEditor.InitializeOnLoadMethod]
        static void DoNullCheck()
        {
            foreach (var type in typeof(Reflection).GetNestedTypes(flags))
            {
                foreach (var field in type.GetFields())
                {
                    if (field.GetValue(null) == null)
                    {
                        Debug.LogError($"{type.Name}.{field.Name} is null! ({Application.unityVersion})");
                    }
                }
            }
        }

        public static class AnimationWindow
        {
            public static Type type;

            /// <summary>
            /// internal AnimationWindowState state {get;}
            /// </summary>
            public static PropertyInfo state;

            /// <summary>
            /// internal bool hasFocus {get;}
            /// </summary>
            public static PropertyInfo hasFocus;

            static AnimationWindow()
            {
                type = Type.GetType("UnityEditor.AnimationWindow,UnityEditor");

                state = type.GetProperty(nameof(state), flags);
                hasFocus = type.GetProperty(nameof(hasFocus), flags);
            }
        }

        public static class AnimationWindowState
        {
            public static Type type;

            /// <summary>
            /// public AnimationClip activeAnimationClip {get;}
            /// </summary>
            public static PropertyInfo activeAnimationClip;

            /// <summary>
            /// public GameObject activeRootGameObject {get;}
            /// </summary>
            public static PropertyInfo activeRootGameObject;

            /// <summary>
            /// public float currentTime {get; set;}
            /// </summary>
            public static PropertyInfo currentTime;

            /// <summary>
            /// private List<DopeLine> m_dopelinesCache;
            /// </summary>
            public static FieldInfo m_dopelinesCache;

            /// <summary>
            /// public bool KeyIsSelected(AnimationWindowKeyframe keyframe) {}
            /// </summary>
            public static MethodInfo KeyIsSelected;

            /// <summary>
            /// public void SelectKey(AnimationWindowKeyframe keyframe) {}
            /// </summary>
            public static MethodInfo SelectKey;

            /// <summary>
            /// public void UnselectKey(AnimationWindowKeyframe keyframe)
            /// </summary>
            public static MethodInfo UnselectKey;

            static AnimationWindowState()
            {
                type = Type.GetType("UnityEditorInternal.AnimationWindowState,UnityEditor");

                activeAnimationClip = type.GetProperty(nameof(activeAnimationClip), flags);
                activeRootGameObject = type.GetProperty(nameof(activeRootGameObject), flags);
                currentTime = type.GetProperty(nameof(currentTime), flags);

                m_dopelinesCache = type.GetField(nameof(m_dopelinesCache), flags);


                KeyIsSelected = type.GetMethod(nameof(KeyIsSelected));
                SelectKey = type.GetMethod(nameof(SelectKey));
                UnselectKey = type.GetMethod(nameof(UnselectKey));
            }
        }

        public static class DopeLine
        {
            public static Type type;

            /// <summary>
            /// public AnimationWindowCurve[] curves {get}
            /// </summary>
            public static PropertyInfo curves;

            /// <summary>
            /// public Type valueType {get;}
            /// </summary>
            public static PropertyInfo valueType;

            /// <summary>
            /// public bool hasChildren;
            /// </summary>
            public static FieldInfo hasChildren;

            /// <summary>
            /// public bool isMasterDopeline;
            /// </summary>
            public static FieldInfo isMasterDopeline;


            static DopeLine()
            {
                type = Type.GetType("UnityEditorInternal.DopeLine,UnityEditor");

                curves = type.GetProperty(nameof(curves), flags);
                valueType = type.GetProperty(nameof(valueType), flags);
                hasChildren = type.GetField(nameof(hasChildren), flags);
                isMasterDopeline = type.GetField(nameof(isMasterDopeline), flags);
            }
        }

        public static class AnimationWindowCurve
        {
            public static Type type;

            /// <summary>
            /// public EditorCurveBinding binding {get;}
            /// </summary>
            public static PropertyInfo binding;

            /// <summary>
            /// public List<AnimationWindowKeyframe> m_Keyframes;
            /// </summary>
            public static FieldInfo m_Keyframes;

            static AnimationWindowCurve()
            {
                type = Type.GetType("UnityEditorInternal.AnimationWindowCurve,UnityEditor");

                binding = type.GetProperty(nameof(binding), flags);

                m_Keyframes = type.GetField(nameof(m_Keyframes), flags);
            }
        }

        public static class AnimationWindowKeyframe
        {
            public static Type type;

            /// <summary>
            /// public float time {get; set;}
            /// </summary>
            public static PropertyInfo time;

            static AnimationWindowKeyframe()
            {
                type = Type.GetType("UnityEditorInternal.AnimationWindowKeyframe,UnityEditor");

                time = type.GetProperty(nameof(time), flags);
            }
        }

    }
}