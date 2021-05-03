using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class KuboLevelEditorWindow : OdinEditorWindow
{
    private static LevelEditor_KuboGrid _kuboGrid => FindObjectOfType<LevelEditor_KuboGrid>();
    [HideInInspector] public EditorAction editorAction;
    [HideInInspector] public ComplexCubeType _cubeType;
    private GameObject _cubeToPlace;

    private GameObject CubeToPlace => (_cubeToPlace)
        ? _cubeToPlace
        : _cubeToPlace =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level Editor/Prefabs/LevelEditorCube.prefab");

    private Event _event;
    private bool _isNone1 = true, _isPlacing, _isRemoving, _isRotating;

    private bool _isNone2 = true,
        _isStatic,
        _isMoveable,
        _isVictory,
        _isDelivery,
        _isElevator,
        _isHeavy,
        _isVictoryHeavy,
        _isMine,
        _isVictoryMine,
        _isCounter,
        _isSwitcher,
        _isVictorySwitcher,
        _isRotator;

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
        _cubeType = (ComplexCubeType) EditorGUILayout.EnumPopup(_cubeType);
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
        ContextMenu_Left();
        ContextMenu_Right();
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
        newCube.ConfigCube(cubeCoords, (CubeType) _cubeType);

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

    void ContextMenu_Left()
    {
        if (_event.control && (_event.type == EventType.MouseDown && _event.button == 0))
        {
            GenericMenu actionMenu = new GenericMenu();

            actionMenu.AddDisabledItem(new GUIContent("Level Editor Action"));

            actionMenu.AddItem(new GUIContent("None"), _isNone1,
                () => { RefreshMenu_Left(EditorAction.None); });

            actionMenu.AddItem(new GUIContent("Place"), _isPlacing,
                () => { RefreshMenu_Left(EditorAction.Place); });

            actionMenu.AddItem(new GUIContent("Remove"), _isRemoving,
                () => { RefreshMenu_Left(EditorAction.Remove); });

            actionMenu.AddItem(new GUIContent("Rotate"), _isRotating,
                () => { RefreshMenu_Left(EditorAction.Rotate); });

            actionMenu.ShowAsContext();

            Repaint();

            _event.Use();
        }
    }

    void ContextMenu_Right()
    {
        if (_event.control && (_event.type == EventType.MouseDown && _event.button == 1))
        {
            GenericMenu cubeMenu = new GenericMenu();

            cubeMenu.AddDisabledItem(new GUIContent("Cube Selection"));

            cubeMenu.AddItem(new GUIContent("None"), _isNone2,
                () => { RefreshMenu_Right(ComplexCubeType.None); });

            cubeMenu.AddItem(new GUIContent("Static"), _isStatic,
                () => { RefreshMenu_Right(ComplexCubeType.Static); });

            cubeMenu.AddItem(new GUIContent("Moveable"), _isMoveable,
                () => { RefreshMenu_Right(ComplexCubeType.Moveable); });

            cubeMenu.AddItem(new GUIContent("Victory"), _isVictory,
                () => { RefreshMenu_Right(ComplexCubeType.MoveableVictory); });

            cubeMenu.AddItem(new GUIContent("Delivery"), _isDelivery,
                () => { RefreshMenu_Right(ComplexCubeType.StaticDelivery); });

            cubeMenu.AddItem(new GUIContent("Elevator"), _isElevator,
                () => { RefreshMenu_Right(ComplexCubeType.StaticElevator); });

            cubeMenu.AddItem(new GUIContent("Heavy"), _isHeavy,
                () => { RefreshMenu_Right(ComplexCubeType.Heavy); });

            cubeMenu.AddItem(new GUIContent("Victory Heavy"), _isVictoryHeavy,
                () => { RefreshMenu_Right(ComplexCubeType.VictoryHeavy); });

            cubeMenu.AddItem(new GUIContent("Mine"), _isMine,
                () => { RefreshMenu_Right(ComplexCubeType.Mine); });

            cubeMenu.AddItem(new GUIContent("Victory Mine"), _isVictoryMine,
                () => { RefreshMenu_Right(ComplexCubeType.VictoryMine); });

            cubeMenu.AddItem(new GUIContent("Counter"), _isCounter,
                () => { RefreshMenu_Right(ComplexCubeType.Counter); });

            cubeMenu.AddItem(new GUIContent("Switcher"), _isSwitcher,
                () => { RefreshMenu_Right(ComplexCubeType.Switcher); });

            cubeMenu.AddItem(new GUIContent("Victory Switcher"), _isVictorySwitcher,
                () => { RefreshMenu_Right(ComplexCubeType.VictorySwitcher); });

            cubeMenu.AddItem(new GUIContent("Rotator"), _isRotator,
                () => { RefreshMenu_Right(ComplexCubeType.Rotator); });

            cubeMenu.ShowAsContext();

            Repaint();

            _event.Use();
        }
    }

    void RefreshMenu_Left(EditorAction _action)
    {
        editorAction = _action;
        _isNone1 = editorAction == EditorAction.None;
        _isPlacing = editorAction == EditorAction.Place;
        _isRemoving = editorAction == EditorAction.Remove;
        _isRotating = editorAction == EditorAction.Rotate;
    }

    void RefreshMenu_Right(ComplexCubeType cubeType)
    {
        _cubeType = cubeType;
        _isNone2 = cubeType == ComplexCubeType.None;
        _isStatic = cubeType == ComplexCubeType.Static;
        _isMoveable = cubeType == ComplexCubeType.Moveable;
        _isVictory = cubeType == ComplexCubeType.MoveableVictory;
        _isDelivery = cubeType == ComplexCubeType.StaticDelivery;
        _isElevator = cubeType == ComplexCubeType.StaticElevator;
        _isHeavy = cubeType == ComplexCubeType.Heavy;
        _isVictoryHeavy = cubeType == ComplexCubeType.VictoryHeavy;
        _isMine = cubeType == ComplexCubeType.Mine;
        _isVictoryMine = cubeType == ComplexCubeType.VictoryMine;
        _isCounter = cubeType == ComplexCubeType.Counter;
        _isSwitcher = cubeType == ComplexCubeType.Switcher;
        _isVictorySwitcher = cubeType == ComplexCubeType.VictorySwitcher;
        _isRotator = cubeType == ComplexCubeType.Rotator;
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