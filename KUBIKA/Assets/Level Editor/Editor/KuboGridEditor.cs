using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Grid_LevelEditor))]
public class KuboGridEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        if (GUILayout.Button("Open Level Creator"))
            KuboLevelEditorWindow.OpenWindow();
    }
}