using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelEditor_KuboGrid))]
public class KuboGridEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        
        if (GUILayout.Button("Open Editor"))
            KuboLevelEditorWindow.OpenWindow(target as LevelEditor_KuboGrid);
    }
}
