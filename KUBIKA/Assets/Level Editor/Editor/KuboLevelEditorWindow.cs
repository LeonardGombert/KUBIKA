using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class KuboLevelEditorWindow : OdinEditorWindow
{
    LevelEditor_KuboGrid kuboGrid;
    [ValueDropdown("Action")] private EditorAction action;
    private GameObject cubeToPlace;
    private Event e;

    public static void OpenWindow(LevelEditor_KuboGrid _kuboGrid)
    {
        KuboLevelEditorWindow window = GetWindow<KuboLevelEditorWindow>();
        window.Show();
        window.kuboGrid = _kuboGrid;
    }

    protected override void OnEnable()
    {
        SceneView.duringSceneGui -= CustomUpdate;
        SceneView.duringSceneGui += CustomUpdate;
    }

    protected override void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Editor Action");
        action = (EditorAction) EditorGUILayout.EnumPopup(action);
        GUILayout.EndHorizontal();
        cubeToPlace = (GameObject) EditorGUILayout.ObjectField(cubeToPlace, typeof(GameObject), true);
    }

    private void CustomUpdate(SceneView sceneView)
    {
        e = Event.current;

        if (action == EditorAction.Place) PlaceCube();
        else if (action == EditorAction.Remove) Remove();
        else if (action == EditorAction.Rotate) Rotate();
    }

    private void PlaceCube()
    {
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(
                Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, e.mousePosition.y, 0)),
                out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Level Editor")))
            {
                GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(cubeToPlace);
                   /* (GameObject) PrefabUtility.InstantiatePrefab(
                        PrefabUtility.GetCorrespondingObjectFromSource(cubeToPlace));*/
                newObject.transform.position = hit.point;
                e.Use();
                
                Undo.RegisterCreatedObjectUndo(newObject, "Undo new Cube");
            }
        }
    }

    private void Remove()
    {
        throw new NotImplementedException();
    }

    private void Rotate()
    {
        throw new NotImplementedException();
    }
}