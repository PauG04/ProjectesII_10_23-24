using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Windows.Editor
{
    public class WindowEditor : EditorWindow
    {
        private Window selectedWindow = null;

        #region Nodes
        [NonSerialized] private GUIStyle nodeStyle;
        //[NonSerialized] private WindowNode draggingNode = null;
        //[NonSerialized] private Vector2 draggingOffset;
        #endregion

        [MenuItem("Editor/Window Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(WindowEditor), false, "Window Editor");
        }
        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID)
        {
            Window window = EditorUtility.InstanceIDToObject(instanceID) as Window;

            if (window != null)
            {
                ShowEditorWindow();
                return true;
            }

            return false;
        }
        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
        }
        private void OnGUI()
        {
            if (selectedWindow == null)
            {
                EditorGUILayout.LabelField("NO WINDOW SELECTED");
                return;
            }
            else
            {

            }
        }
        private void OnSelectionChanged()
        {
            Window newDialogue = Selection.activeObject as Window;

            if (newDialogue != null)
            {
                selectedWindow = newDialogue;
                Repaint();
            }
        }
    }
}