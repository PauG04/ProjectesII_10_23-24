using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        WindowDataObject obj = EditorUtility.InstanceIDToObject(instanceId) as WindowDataObject;

        if (obj != null)
        {
            GameDataObjectEditorWindow.Open(obj);
            return true;
        }

        return false;
    }
}

[CustomEditor(typeof(WindowDataObject))]
public class GameDataObjectCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Editor Windows"))
        {
            GameDataObjectEditorWindow.Open((WindowDataObject)target);
        }
    }
}
