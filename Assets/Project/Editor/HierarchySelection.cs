using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    static class HierarchySelection
    {
        static bool runSelectChildren = true;

        [MenuItem("GameObject/Deselect", true)]
        public static bool ValidateDeselect()
        {
            return Selection.objects != null && Selection.objects.Length > 0;
        }
        
        [MenuItem("GameObject/Deselect &d", false, -1000)]
        public static void Deselect()
        {
            Selection.objects = null;
        }
        
        [MenuItem("GameObject/Collapse Parent", true)]
        public static bool ValidateCollapseParent()
        {
            // runSelectChildren = true;
            return SelectionHasChildren();
        }
        
        [MenuItem("GameObject/Collapse Parent &c", false, -1000)]
        public static void CollapseParent()
        {
            // Find parent 
            var parent = Selection.activeGameObject.transform.parent;
            
            if (parent != null)
            {
                // If parent has children, collapse it
                if (parent.childCount > 0)
                {
                    // If parent is already expanded, collapse it
                    SetExpanded(parent.gameObject, false);
                }
            }
            
            // if (runSelectChildren)
            // {
            //   
            // }
            //
            // runSelectChildren = false;
        }
        
        // [MenuItem("GameObject/Toggle Active State &a", false)]
        // public static void ToggleActiveState()
        // {
        //     foreach (var obj in Selection.gameObjects)
        //     {
        //         obj.SetActive(!obj.activeSelf);
        //     }
        // }

        [MenuItem("GameObject/Select All Children &s", false)]
        public static void SelectAllChildren()
        {
            var selectedObjects = new List<GameObject>();
            foreach (var obj in Selection.gameObjects)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    selectedObjects.Add(obj.transform.GetChild(i).gameObject);
                }
            }
            Selection.objects = selectedObjects.ToArray();
        }
        
        /// <summary>
        /// Selects the first child of every selected object.
        /// </summary>
        [MenuItem("GameObject/Select First Child", false, -1000)]
        public static void SelectFirstChildren()
        {
            if (runSelectChildren) // Prevents function from being executed twice
            {
                //Expand current selection to show the children you're about to select
                foreach (var obj in Selection.gameObjects)
                {
                    SetExpanded(obj, true);
                }

                //Select every first child
                List<GameObject> objects = new List<GameObject>();

                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    var obj = Selection.gameObjects[i].transform;

                    if (obj.childCount > 0)
                        objects.Add(obj.GetChild(0).gameObject);
                }

                Selection.objects = objects.ToArray();
            }

            runSelectChildren = false;
        }

        /// <summary>
        /// Disables the "Select First Child" button if the currently selected objects have no children to select
        /// </summary>
        [MenuItem("GameObject/Select First Child", true)]
        public static bool ValidateSelectChildren()
        {
            runSelectChildren = true;
            return SelectionHasChildren();
        }

        /// <summary>
        /// Collapses the selected objects
        /// </summary>
        [MenuItem("GameObject/Collapse Selection", false, -800)]
        public static void CollapseSelection()
        {
            if (runSelectChildren) // Prevents function from being executed twice
            {
                foreach (var obj in Selection.gameObjects)
                {
                    SetExpanded(obj, false);
                }
            }

            runSelectChildren = false;
        }

        /// <summary>
        /// Disables the "Collapse Selection" button if the currently selected objects have nothing to collapse
        /// </summary>
        [MenuItem("GameObject/Collapse Selection", true)]
        public static bool ValidateCollapseSelection()
        {
            runSelectChildren = true;
            return SelectionHasChildren();
        }

        /// <summary>
        /// Expands the selected objects
        /// </summary>
        [MenuItem("GameObject/Expand Selection", false, -800)]
        public static void ExpandSelection()
        {
            if (runSelectChildren) // Prevents function from being executed twice
            {
                foreach (var obj in Selection.gameObjects)
                {
                    SetExpanded(obj, true);
                }
            }

            runSelectChildren = false;
        }

        /// <summary>
        /// Disables the "Expand Selection" button if the currently selected objects have nothing to expand
        /// </summary>
        [MenuItem("GameObject/Expand Selection", true)]
        public static bool ValidateExpandSelection()
        {
            runSelectChildren = true;
            return SelectionHasChildren();
        }

        /// <summary>
        /// Checks if any of the selected gameobjects in the Hierarchy have children
        /// </summary>
        static bool SelectionHasChildren()
        {
            foreach (var obj in Selection.gameObjects)
            {
                if (obj.transform.childCount > 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Expand or collapse object in Hierarchy recursively
        /// </summary>
        /// <param name="obj">The object to expand or collapse</param>
        /// <param name="expand">A boolean to indicate if you want to expand or collapse the object</param>
        public static void SetExpandedRecursive(GameObject obj, bool expand)
        {
            var methodInfo = GetHierarchyWindowType().GetMethod("SetExpandedRecursive");

            methodInfo.Invoke(GetHierarchyWindow(), new object[] { obj.GetInstanceID(), expand });
        }

        /// <summary>
        ///  Expand or collapse object in Hierarchy
        /// </summary>
        /// <param name="obj">The object to expand or collapse</param>
        /// <param name="expand">A boolean to indicate if you want to expand or collapse the object</param>
        public static void SetExpanded(GameObject obj, bool expand)
        {
            object sceneHierarchy = GetHierarchyWindowType().GetProperty("sceneHierarchy").GetValue(GetHierarchyWindow());
            var methodInfo = sceneHierarchy.GetType().GetMethod("ExpandTreeViewItem", BindingFlags.NonPublic | BindingFlags.Instance);

            methodInfo.Invoke(sceneHierarchy, new object[] { obj.GetInstanceID(), expand });
        }

        static Type GetHierarchyWindowType()
        {
            return typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        }

        static EditorWindow GetHierarchyWindow()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
            return EditorWindow.focusedWindow;
        }
    }
}