using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScriptBoy.MotionPathAnimEditor
{
    class AnimEditor
    {
        public static AnimationWindowWrapper animationWindow;

        public static AnimationClip animationClip;
        public static GameObject rootGameObject;
        public static Transform rootTransform;
        public static MotionPath rootMotionPath;

        public List<MotionPath> motionPaths;

        public Dictionary<GameObject, MotionPath[]> history;

        public bool editMode;

        public AnimEditor()
        {
            motionPaths = new List<MotionPath>();
            history = new Dictionary<GameObject, MotionPath[]>();
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui +=  OnSceneGUI;
#else
            SceneView.onSceneGUIDelegate += OnSceneGUI;
#endif
            Undo.undoRedoPerformed += UndoRedoPerformed;
        }

        public void Destroy()
        {
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui -=  OnSceneGUI;
#else
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
#endif
            Undo.undoRedoPerformed -= UndoRedoPerformed;
        }

        private void UndoRedoPerformed()
        {
            if (GetAnimationInfo())
            {
                SampleAnimation(animationWindow.time);
            }
        }

        #region OnSceneGUI
        private void OnSceneGUI(SceneView sceneView)
        {
            if (!GetAnimationInfo()) return;

            if (Settings.syncTimeRange)
            {
                TimeRange.SyncWithCurrentTime();
            }


            if (motionPaths == null) return;
            if (motionPaths.Count == 0) return;

            BeginEditor();
            DoEditor(sceneView);
            EndEditor();
        }

        private void BeginEditor()
        {
            foreach (var motionPath in motionPaths)
            {
                if (motionPath.active)
                {
                    motionPath.FixMissingKeyframes();
                    motionPath.UpdateHandleCount();
                }
            }

            if (Settings.pathSpace == Space.World)
            {
                if (editMode || !editMode && GUIUtility.hotControl == 0)
                {

                    float defaultTime = animationWindow.time;

                    int motionPathCount = motionPaths.Count;

                    int frameCount = (int)(animationClip.length * animationClip.frameRate);
                    float frameToTime = 1 / animationClip.frameRate;
                    float time2Frame = animationClip.frameRate;
                    HashSet<int> hotFrames = new HashSet<int>();

                    for (int i = 0; i < motionPathCount; i++)
                    {
                        var motionPath = motionPaths[i];
                        if (motionPath.active) motionPath.StartCachingWorldPath(frameCount, time2Frame, hotFrames);
                    }
                    int skipFrame = frameCount / 60;
                    if (skipFrame <= 0) skipFrame = 1;

                    for (int frame = 0; frame <= frameCount; frame++)
                    {
                        float time = frame * frameToTime;

                        if (Settings.useTimeRange && !TimeRange.Contains(time)) continue;

                        if (frame != 0 && frame != frameCount)
                        {
                            if (frame % skipFrame != 0 && !hotFrames.Contains(frame)) continue;
                        }

          
                        SampleAnimation(time);

                        for (int i = 0; i < motionPathCount; i++)
                        {
                            var motionPath = motionPaths[i];
                            if (motionPath.active) motionPath.CacheWorldPosition(frame, frameCount);
                        }
                    }

                    SampleAnimation(defaultTime);
                }

                foreach (var motionPath in motionPaths)
                {
                    if (motionPath.active)
                    {
                        motionPath.UpdateHandles();
                    }
                }
            }
            else
            {
                foreach (var motionPath in motionPaths)
                {
                    if (motionPath.active)
                    {
                        motionPath.UpdateHandlesMatrix();
                        motionPath.UpdateHandles();
                    }
                }
            }

            foreach (var motionPath in motionPaths)
            {
                if (motionPath.active)
                {
                    motionPath.SyncSelection();
                }
            }
        }

        private void SampleAnimation(float time)
        {
            animationClip.SampleAnimation(rootGameObject, time);
        }

        private void EndEditor()
        {
            if (!GUI.changed) return;

            ApplyChages();
            AnimEditorWindow.RepaintWindow();
        }

        public void ApplyChages()
        {
            animationClip.SampleAnimation(rootGameObject, animationWindow.time);

            foreach (var motionPath in motionPaths)
            {
                if (motionPath.active && motionPath.editable)
                {
                    motionPath.ApplyChages();
                }
            }
        }

        private void DoEditor(SceneView sceneView)
        {
            if (sceneView.in2DMode && Settings.useLocalSnappingIn2D && EditorGridUtility.IsSnapActive && HandleSelection.count < 2)
            {
                GridRenderer.Draw();
            }

            DrawMotionPaths();

            if (editMode) EditMotionPaths();
        }

        private void DrawMotionPaths()
        {
            if (Settings.pathSpace == Space.World)
            {
                foreach (var motionPath in motionPaths)
                {
                    if (motionPath.active)
                    {
                        motionPath.DrawPath();
                    }
                }
            }
            else
            {
                foreach (var motionPath in motionPaths)
                {
                    if (motionPath.active)
                    {
                        motionPath.DrawCurves();
                    }
                }

            }
        }

        private void EditMotionPaths()
        {
            int mouseControlID = GUIUtility.GetControlID(FocusType.Passive);
   
            if (HandleSelectionRect.IsActive)
            {
                DoBoxSelection();
            }

            var EVENT = Event.current;
            bool startMagnet = Settings.useMagnet && (Magnet.controls || Magnet.tangents) && EVENT.isMouse && EVENT.button == 0 && EVENT.type == EventType.MouseDown;

            if (HandleSelection.count > 1)
            {
                EditSelection();
            }
            else
            {
                EditMono();
            }

            if (Settings.useMagnet && (Tools.current != Tool.Rotate || Tools.current != Tool.Scale))
            {
                foreach (var m in motionPaths)
                {
                    if (startMagnet)
                    {
                        m.StartMagnet();
                    }
                    else
                    {
                        m.UpdateMagnet();
                    }
                }
            }

            if (EVENT.isMouse && EVENT.button == 0)
            {
                if (EVENT.type == EventType.MouseDown && EVENT.button == 0)
                {
                    GUIUtility.hotControl = mouseControlID;
                    EVENT.Use();
                    HandleSelectionRect.Start();
                }

                if (EVENT.type == EventType.MouseUp)
                {
                    EVENT.Use();
                    GUIUtility.hotControl = 0;

                    if (HandleSelectionRect.IsActive)
                    {
                        HandleSelectionRect.Stop();
                        AnimEditorWindow.RepaintWindow();
                    }
                    else
                    {
                        HandleSelection.Clear();
                    }
                }
            }

            if (HandleSelectionRect.IsActive)
            {
                HandleSelectionRect.Draw();
                if (GUIUtility.hotControl != mouseControlID)
                {
                    HandleSelectionRect.Stop();
                }
            }
        }

        private void DoBoxSelection()
        {
            HandleSelectionRect.Update();

            foreach (var motionPath in motionPaths)
            {
                if (motionPath.IsEditable)
                {
                    motionPath.CheckBoxSelection();
                }
            }
        }

        private void EditMono()
        {
            if (SceneViewUtility.Is2DMode(SceneView.currentDrawingSceneView))
            {
                foreach (var motionPath in motionPaths)
                {
                    if (motionPath.IsEditable) motionPath.EditCurves2D();
                }
            }
            else
            {
                foreach (var motionPath in motionPaths)
                {
                    if (motionPath.IsEditable) motionPath.EditCurves3D();
                }
            }
        }

        private void EditSelection()
        {
            DrawHandleCaps();

            HandleSelectionTransform.instance.CheckTimeChanges(animationWindow.time);

            switch (Tools.current)
            {
                case Tool.Move:
                    HandleSelectionTransform.instance.DoPositionHandle();
                    break;
                case Tool.Rotate:
                    HandleSelectionTransform.instance.DoRotationHandle();
                    break;
                case Tool.Scale:
                    HandleSelectionTransform.instance.DoScaleHandle();
                    break;
            }
        }

        private void DrawHandleCaps()
        {
            foreach (var motionPath in motionPaths)
            {
                if (motionPath.IsEditable)
                {
                    motionPath.DrawSelectionButtons();
                }
            }
        }
        #endregion

        #region Get Animation Info
        private bool GetAnimationInfo()
        {
            //Get AnimationWindow
            if (!animationWindow)
            {
                animationWindow = AnimationWindowWrapper.FindWindow();
                AnimEditorWindow.RepaintWindow();
                return false;
            } else animationWindow.RefreshState();



            //Get AnimationClip
            var newAnimationClip = animationWindow.animationClip;
            if (animationClip != newAnimationClip)
            {
                animationClip = newAnimationClip;
                HandleSelection.Clear();
                AnimEditorWindow.RepaintWindow();
            }
            if (animationClip == null) return false;


            //Get RootGameObject 
            var newRootGameObject = animationWindow.rootGameObject;
            if (newRootGameObject == null)
            {
                AnimEditorWindow.RepaintWindow();
                return false;
            }
            if (rootGameObject != newRootGameObject)
            {
                if (rootGameObject != null)
                {
                    history.Remove(rootGameObject);
                    history.Add(rootGameObject, motionPaths.ToArray());
                    motionPaths.Clear();
                }

                rootGameObject = newRootGameObject;
                if (rootGameObject != null)
                {
                    rootTransform = rootGameObject.transform;
                    if (history.ContainsKey(rootGameObject))
                    {
                        motionPaths.AddRange(history[rootGameObject]);
                        history.Remove(rootGameObject);
                    }
                }
                AnimEditorWindow.RepaintWindow();
            }

            if (rootGameObject == null) return false;

            return UpdateMotionPathsCurvesInfo();
        }

        private bool UpdateMotionPathsCurvesInfo()
        {
            motionPaths.RemoveAll(m => m.transform == null);

            foreach (var motionPath in motionPaths) motionPath.ClearAnimationCurves();

            var positionCurveBindings = PositionCurveBindingsCollector.GetList(animationClip, rootTransform);

            if (!animationWindow.FindAnimationWindowKeyframes(positionCurveBindings)) return false;

            foreach (var motionPath in motionPaths)
            {
                foreach (var positionCurveBinding in positionCurveBindings)
                {
                    if (motionPath.transform == positionCurveBinding.transform)
                    {
                        motionPath.SetAnimationCurves(positionCurveBinding);
                        break;
                    }
                }
            }

            rootMotionPath = motionPaths.Find(m => m.transform == rootTransform);

            return true;
        }
        #endregion
    }
}
