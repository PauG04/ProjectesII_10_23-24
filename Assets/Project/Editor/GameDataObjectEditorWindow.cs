using UnityEditor;
using UnityEngine;

public class GameDataObjectEditorWindow : ExtendedEditorWindow
{
    public static void Open(WindowDataObject dataObject)
    {
        GameDataObjectEditorWindow window = GetWindow<GameDataObjectEditorWindow>("Window Data Editor");
        window.serializedObject = new SerializedObject(dataObject);
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("values");
        DrawProperties(currentProperty, true);
        Apply();
    }
}
