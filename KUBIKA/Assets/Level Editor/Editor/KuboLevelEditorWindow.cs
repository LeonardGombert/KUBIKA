using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class KuboLevelEditorWindow : OdinEditorWindow
{
    private Grid_LevelEditor LevelEditorGrid => FindObjectOfType<Grid_LevelEditor>();
    private Transform gridParentObj => LevelEditorGrid.transform;
    private EditorAction _editorAction;
    private ComplexCubeType _placingCubeType;
    private ComplexCubeType _startingCubeType;
    private GameObject _cubeToPlace => AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level Editor/Prefabs/LevelEditorCube.prefab");

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
        EditorGUILayout.LabelField("Editor Action", GUILayout.MaxWidth(128));
        _editorAction = (EditorAction) EditorGUILayout.EnumPopup(_editorAction);
        GUILayout.EndHorizontal();

        // Current Cube Type
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Placing Cube", GUILayout.MaxWidth(128));
        _placingCubeType = (ComplexCubeType) EditorGUILayout.EnumPopup(_placingCubeType);
        GUILayout.EndHorizontal();


        EditorGUILayout.Space(); // Space


        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Starting Cube", GUILayout.MaxWidth(128));
        _startingCubeType = (ComplexCubeType) EditorGUILayout.EnumPopup(_startingCubeType);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Editor Cube", GUILayout.MaxWidth(128));
        EditorGUILayout.ObjectField(_cubeToPlace, typeof(GameObject), true);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Grid Object", GUILayout.MaxWidth(128));
        EditorGUILayout.ObjectField(LevelEditorGrid.gameObject, typeof(GameObject), true);
        GUILayout.EndHorizontal();


        EditorGUILayout.Space(); // Space


        // Level Utility Methods
        if (GUILayout.Button("New Level"))
            NewLevel();
        
        if (GUILayout.Button("Save Level"))
            SaveLevel();

        if (GUILayout.Button("Open Level"))
            OpenLevel();
        
        EditorGUILayout.Space(); // Space


        GUILayout.EndVertical();
    }

    private void CustomUpdate(SceneView sceneView)
    {
        _event = Event.current;
        RegisterActions();
        ContextMenu_Left();
        ContextMenu_Right();
    }

    #endregion

    #region Editor Actions

    private void RegisterActions()
    {
        if (_editorAction == EditorAction.None) return;
        if (_event.type != EventType.MouseDown || _event.button != 0) return;

        if (Physics.Raycast(
            Camera.current.ScreenPointToRay(new Vector3(_event.mousePosition.x,
                Camera.current.pixelHeight - _event.mousePosition.y, 0)),
            out var hit, Mathf.Infinity, ~LayerMask.NameToLayer("Level Editor")))
        {
            switch (_editorAction)
            {
                case EditorAction.Place:
                    PlaceCube(hit);
                    break;
                case EditorAction.Remove:
                    RemoveCube(hit);
                    break;
                case EditorAction.Rotate:
                    RotateCube(hit);
                    break;
                case EditorAction.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            UpdateGrid();
        }
    }

    private void PlaceCube(RaycastHit hit)
    {
        GameObject newObject = (GameObject) PrefabUtility.InstantiatePrefab(_cubeToPlace);

        var newCube = newObject.GetComponent<CubeObject_LevelEditor>();
        var hitCube = hit.collider.GetComponent<CubeObject_LevelEditor>();

        Vector3 newIndex = hitCube.Index.Pos[0] + hit.normal;
        GridCoord cubeCoords = new GridCoord((int) newIndex.x, (int) newIndex.y, (int) newIndex.z);

        // set cube type and data
        newCube.ConfigCube(cubeCoords, _placingCubeType);

        LevelEditorGrid.placedCubes.Add(newObject.GetComponent<AbstractCubeObject>());

        newObject.transform.position = hit.transform.position + hit.normal * LevelEditorGrid.width;
        newObject.transform.parent = gridParentObj;

        Undo.RegisterCreatedObjectUndo(newObject, "Undo New Cube");

        _event.Use();
    }

    private void RemoveCube(RaycastHit hit)
    {
        GameObject destroyed = hit.transform.gameObject;

        LevelEditorGrid.placedCubes.Remove(destroyed.GetComponent<AbstractCubeObject>());

        Undo.DestroyObjectImmediate(destroyed);

        DestroyImmediate(destroyed);
        _event.Use();
    }

    private void RotateCube(RaycastHit hit)
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
        _editorAction = _action;
        _isNone1 = _editorAction == EditorAction.None;
        _isPlacing = _editorAction == EditorAction.Place;
        _isRemoving = _editorAction == EditorAction.Remove;
        _isRotating = _editorAction == EditorAction.Rotate;
    }

    void RefreshMenu_Right(ComplexCubeType complexCubeType)
    {
        _placingCubeType = complexCubeType;
        _isNone2 = complexCubeType == ComplexCubeType.None;
        _isStatic = complexCubeType == ComplexCubeType.Static;
        _isMoveable = complexCubeType == ComplexCubeType.Moveable;
        _isVictory = complexCubeType == ComplexCubeType.MoveableVictory;
        _isDelivery = complexCubeType == ComplexCubeType.StaticDelivery;
        _isElevator = complexCubeType == ComplexCubeType.StaticElevator;
        _isHeavy = complexCubeType == ComplexCubeType.Heavy;
        _isVictoryHeavy = complexCubeType == ComplexCubeType.VictoryHeavy;
        _isMine = complexCubeType == ComplexCubeType.Mine;
        _isVictoryMine = complexCubeType == ComplexCubeType.VictoryMine;
        _isCounter = complexCubeType == ComplexCubeType.Counter;
        _isSwitcher = complexCubeType == ComplexCubeType.Switcher;
        _isVictorySwitcher = complexCubeType == ComplexCubeType.VictorySwitcher;
        _isRotator = complexCubeType == ComplexCubeType.Rotator;
    }

    #endregion

    #region Utility Functions

    private void NewLevel()
    {
        if (EditorUtility.DisplayDialog("Create a new Level ? ", "This will wipe any unsaved changes or progress in the scene.",
            "Create", "Cancel"))
        {
            LevelEditorGrid.ClearNodes();

            GameObject newObject = (GameObject) PrefabUtility.InstantiatePrefab(_cubeToPlace);

            var newCube = newObject.GetComponent<CubeObject_LevelEditor>();

            // set cube type and data
            newCube.ConfigCube(GridCoord.Zero, _startingCubeType);

            LevelEditorGrid.placedCubes.Add(newCube);

            newObject.transform.position = Vector3.zero;
            newObject.transform.parent = gridParentObj;
        }
    }
    
    private void CookLevel() => LevelEditorGrid.GenerateNodes();

    private void SaveLevel()
    {
        CookLevel();    
        throw new NotImplementedException();
    }
    
    private void OpenLevel()
    {
        throw new NotImplementedException();
    }

    
    private void UpdateGrid()
    {
        int sizeX = 0, sizeY = 0, sizeZ = 0;
        
        for (int i = 0; i < LevelEditorGrid.placedCubes.Count; i++)
        {
            if (LevelEditorGrid.placedCubes[i])
            {
                sizeX = sizeX < LevelEditorGrid.placedCubes[i].Index.Pos[0].x ?(int)LevelEditorGrid.placedCubes[i].Index.Pos[0].x : sizeX;
                sizeY = sizeY < LevelEditorGrid.placedCubes[i].Index.Pos[0].y ?(int)LevelEditorGrid.placedCubes[i].Index.Pos[0].y : sizeY;
                sizeZ = sizeZ < LevelEditorGrid.placedCubes[i].Index.Pos[0].z ?(int)LevelEditorGrid.placedCubes[i].Index.Pos[0].z : sizeZ;
                continue;
            }
            LevelEditorGrid.placedCubes.RemoveAt(i--);
        }

        LevelEditorGrid.sizeX = sizeX + 1;
        LevelEditorGrid.sizeY = sizeY + 1;
        LevelEditorGrid.sizeZ = sizeZ + 1;
    }
    
    #endregion
}