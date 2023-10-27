using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Linq;

namespace Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        private Dialogue selectedDialogue = null;

        #region Nodes
        [NonSerialized] private GUIStyle nodeStyle;
        [NonSerialized] private GUIStyle playerNodeStyle;
        [NonSerialized] private DialogueNode draggingNode = null;
        [NonSerialized] private Vector2 draggingOffset;
        #endregion

        #region Buttons
        [NonSerialized] private DialogueNode creatingNode = null;
        [NonSerialized] private DialogueNode deletingNode = null;
        [NonSerialized] private DialogueNode linkParentNode = null;
        #endregion

        #region Scroll
        private Vector2 scrollPosition;
        [NonSerialized] bool dragingCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;
        #endregion

        const float canvasSize = 4000;
        const float backgroundSize = 50;

        [MenuItem("Editor/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }
        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;

            if (dialogue != null)
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

            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            playerNodeStyle.normal.textColor = Color.white;
            playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
        }
        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;

            if (newDialogue != null )
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }
        private void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("NO DIALOGUE SELECTED");
                return;
            } 
            else 
            { 
                ProcessEvent();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTexture = Resources.Load("background") as Texture2D;
                Rect textureCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTexture, textureCoords);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (deletingNode != null)
                {
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }
            }
        }
        private void ProcessEvent()
        {
            if(Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    dragingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedDialogue, "Move Dialogue Node");
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && dragingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseDrag && dragingCanvas)
            {
                dragingCanvas = false;
            }
        }
        public void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;

            if (node.IsPlayerSpeaking())
            {
                style = playerNodeStyle;
            }

            GUILayout.BeginArea(node.GetRect(), style);
            EditorGUI.BeginChangeCheck();

            node.SetText(EditorGUILayout.TextField(node.GetText()));

            DrawLinkButton(node);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("-"))
            {
                deletingNode = node;
            }
            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
        private void DrawLinkButton(DialogueNode node)
        {
            if (linkParentNode == null)
            {
                if (GUILayout.Button("LINK"))
                {
                    linkParentNode = node;
                }
            }
            else if(linkParentNode == node)
            {
                if (GUILayout.Button("CANCEL"))
                {
                    linkParentNode = null;
                }
            }
            else if(linkParentNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("UNLINK"))
                {
                    linkParentNode.RemoveChild(node.name);
                    linkParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("CHILD"))
                {
                    Undo.RecordObject(selectedDialogue, "Add Dialogue Link");
                    linkParentNode.AddChild(node.name);
                    linkParentNode = null;
                }
            }
        }
        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);

            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;

                Handles.DrawBezier(
                    startPosition, endPosition, 
                    startPosition + controlPointOffset, 
                    endPosition - controlPointOffset, 
                    Color.white, null, 4f
                );
            }
        }
        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;

            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.GetRect().Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }
    }
}