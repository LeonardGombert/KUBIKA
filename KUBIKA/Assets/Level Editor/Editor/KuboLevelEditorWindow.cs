using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class KuboLevelEditorWindow : OdinEditorWindow
{
    private static LevelEditor_KuboGrid _kuboGrid => FindObjectOfType<LevelEditor_KuboGrid>();
    [HideInInspector] public EditorAction editorAction;
    [HideInInspector] public CubeType cubeType;
    private GameObject _cubeToPlace;

    private GameObject CubeToPlace => (_cubeToPlace)
        ? _cubeToPlace
        : _cubeToPlace =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level Editor/Prefabs/LevelEditorCube.prefab");

    private Event _event;
    private bool _isNone = true, _isPlacing, _isRemoving, _isRotating;

    #region Unity Editor Functions

    public static void OpenWindow()
    {
        var window = GetWindow<KuboLevelEditorWindow>();
        window.Show();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        SceneView.duringSceneGui -= CustomUpdate;
        SceneView.duringSceneGui += CustomUpdate;
    }

    protected override void OnGUI()
    {
        GUILayout.BeginVertical();

        // Current Editor Action
        GUILayout.BeginHorizontal();
        GUILayout.Label("Editor Action");
        editorAction = (EditorAction) EditorGUILayout.EnumPopup(editorAction);
        GUILayout.EndHorizontal();

        // Current Cube Type
        GUILayout.BeginHorizontal();
        GUILayout.Label("Placing Cube");
        cubeType = (CubeType) EditorGUILayout.EnumPopup(cubeType);
        GUILayout.EndHorizontal();

        EditorGUILayout.ObjectField(CubeToPlace, typeof(GameObject), true);

        // Level Utility Methods
        if (GUILayout.Button("Cook Level"))
            CookLevel();

        if (GUILayout.Button("Clear Level"))
        {
            if (EditorUtility.DisplayDialog("Clear Level ? ", "Are you sure that you want to clear the entire level ?",
                "Clear Level", "Cancel"))
            {
                ClearLevel();
            }
        }

        GUILayout.EndVertical();
    }

    private void CustomUpdate(SceneView sceneView)
    {
        _event = Event.current;
        RegisterActions();
        ContextMenu();
        UpdateGrid();
    }

    #endregion

    #region Editor Actions

    private void RegisterActions()
    {
        if (editorAction == EditorAction.None) return;
        if (_event.type != EventType.MouseDown || _event.button != 0) return;

        if (Physics.Raycast(
            Camera.current.ScreenPointToRay(new Vector3(_event.mousePosition.x,
                Camera.current.pixelHeight - _event.mousePosition.y, 0)),
            out var hit, Mathf.Infinity, ~LayerMask.NameToLayer("Level Editor")))
        {
            switch (editorAction)
            {
                case EditorAction.Place:
                    PlaceCube(hit);
                    break;
                case EditorAction.Remove:
                    Remove(hit);
                    break;
                case EditorAction.Rotate:
                    Rotate(hit);
                    break;
                case EditorAction.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void PlaceCube(RaycastHit hit)
    {
        GameObject newObject = (GameObject) PrefabUtility.InstantiatePrefab(CubeToPlace);

        var newCube = newObject.GetComponent<Cube_LevelEditor>();
        var hitCube = hit.collider.GetComponent<Cube_LevelEditor>();

        Vector3 newIndex = hitCube.Index.Config[0] + hit.normal;
        KuboVector cubeCoords = new KuboVector((int) newIndex.x, (int) newIndex.y, (int) newIndex.z);

        // set cube type and data
        newCube.ConfigCube(cubeCoords, cubeType);

        _kuboGrid.placedCubes.Add(newCube);

        newObject.transform.position = hit.transform.position + hit.normal * _kuboGrid.width;

        Undo.RegisterCreatedObjectUndo(newObject, "Undo New Cube");

        _event.Use();
    }

    private void Remove(RaycastHit hit)
    {
        GameObject destroyed = hit.transform.gameObject;

        _kuboGrid.placedCubes.Remove(destroyed.GetComponent<AbstractCubeObject>());

        Undo.DestroyObjectImmediate(destroyed);

        DestroyImmediate(destroyed);
        _event.Use();
    }

    private void Rotate(RaycastHit hit)
    {
    }

    #endregion

    #region Dropdown Menu

    void ContextMenu()
    {
        if (_event.control && (_event.type == EventType.MouseDown && _event.button == 1))
        {
            GenericMenu myMenu = new GenericMenu();

            myMenu.AddDisabledItem(new GUIContent("Level Editor Action"));

            myMenu.AddItem(new GUIContent("None"), _isNone,
                () => { RefreshMenu(EditorAction.None); });

            myMenu.AddItem(new GUIContent("Place"), _isPlacing,
                () => { RefreshMenu(EditorAction.Place); });

            myMenu.AddItem(new GUIContent("Remove"), _isRemoving,
                () => { RefreshMenu(EditorAction.Remove); });

            myMenu.AddItem(new GUIContent("Rotate"), _isRotating,
                () => { RefreshMenu(EditorAction.Rotate); });

            myMenu.ShowAsContext();

            Repaint();

            _event.Use();
        }
    }

    void RefreshMenu(EditorAction _action)
    {
        editorAction = _action;
        _isNone = editorAction == EditorAction.None;
        _isPlacing = editorAction == EditorAction.Place;
        _isRemoving = editorAction == EditorAction.Remove;
        _isRotating = editorAction == EditorAction.Rotate;
    }

    #endregion

    #region Utility Functions

    private void ClearLevel()
    {
        _kuboGrid.ClearGrid();
    }

    private void CookLevel()
    {
        _kuboGrid.BuildGrid();
    }

    private void UpdateGrid()
    {
        for (int i = 0; i < _kuboGrid.placedCubes.Count; i++)
        {
            if (!_kuboGrid.placedCubes[i])
                _kuboGrid.placedCubes.RemoveAt(i--);
        }
    }

    #endregion
}