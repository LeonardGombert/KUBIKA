using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public class KuboLevelEditorWindow : OdinEditorWindow
{
    LevelEditor_KuboGrid kuboGrid;
    [HideInInspector] public EditorAction action;
    private GameObject cubeToPlace;
    private Event e;
    private bool isNone = true, isPlacing, isRemoving, isRotating;


    public static void OpenWindow(LevelEditor_KuboGrid _kuboGrid)
    {
        KuboLevelEditorWindow window = GetWindow<KuboLevelEditorWindow>();
        window.Show();
        window.kuboGrid = _kuboGrid;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

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

        OnSceneGUI();
    }

    #region Editor Actions

    private void PlaceCube()
    {
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(
                Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, e.mousePosition.y, 0)),
                out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Level Editor")))
            {
                GameObject newObject = (GameObject) PrefabUtility.InstantiatePrefab(cubeToPlace);
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
    }

    private void Rotate()
    {
    }

    #endregion

    #region Dropdown Menu

    void OnSceneGUI()
    {
        if (e.type == EventType.MouseDown && e.button == 1)
        {
            GenericMenu myMenu = new GenericMenu();

            myMenu.AddDisabledItem(new GUIContent("Level Editor Action"));

            myMenu.AddItem(new GUIContent("None"), isNone,
                () => { RefreshMenu(EditorAction.None); });

            myMenu.AddItem(new GUIContent("Place"), isPlacing,
                () => { RefreshMenu(EditorAction.Place); });

            myMenu.AddItem(new GUIContent("Remove"), isRemoving,
                () => { RefreshMenu(EditorAction.Remove); });

            myMenu.AddItem(new GUIContent("Rotate"), isRotating,
                () => { RefreshMenu(EditorAction.Rotate); });

            myMenu.ShowAsContext();

            Repaint();

            e.Use();
        }
    }

    void RefreshMenu(EditorAction _action)
    {
        action = _action;
        isNone = action == EditorAction.None;
        isPlacing = action == EditorAction.Place;
        isRemoving = action == EditorAction.Remove;
        isRotating = action == EditorAction.Rotate;
    }

    #endregion
}