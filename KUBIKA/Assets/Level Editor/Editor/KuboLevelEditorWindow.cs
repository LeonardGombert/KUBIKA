using System;
using System.IO;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class KuboLevelEditorWindow : OdinEditorWindow
{
    private static Grid_LevelEditor LevelEditorGrid => FindObjectOfType<Grid_LevelEditor>();

    private static CubePoolManager_LevelEditor PoolManager =>
        FindObjectOfType<CubePoolManager_LevelEditor>();

    private static LevelSaver_Editor LevelSaver => FindObjectOfType<LevelSaver_Editor>();
    private static LevelLoader_Editor LevelLoader => FindObjectOfType<LevelLoader_Editor>();
    private static Transform GridParentObj => LevelEditorGrid.transform;

    private static GameObject CubeToPlace =>
        AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level Editor/Prefabs/Editor_Cube.prefab");

    private EditorAction _editorAction;
    private ComplexCubeType _placingCubeType;
    private ComplexCubeType _startingCubeType;
    private string _levelName;
    private string _currentSavedLevelName;

    private TextAsset _levelFile;

    private Event _event;
    private bool _rightIsNone = true, _isPlacing, _isRemoving, _isRotating;

    private bool _leftIsNone = true,
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
        window.ClearLevel();
    }

    protected override void OnEnable()
    {
        SceneView.duringSceneGui -= CustomUpdate;
        SceneView.duringSceneGui += CustomUpdate;
    }

    private void OnBecameInvisible()
    {
        LevelEditorGrid.ClearNodes();
        
        _currentSavedLevelName = null;
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
        EditorGUILayout.ObjectField(CubeToPlace, typeof(GameObject), true);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Grid Object", GUILayout.MaxWidth(128));
        EditorGUILayout.ObjectField(LevelEditorGrid.gameObject, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(); // Space

        if (GUILayout.Button("Clear Level"))
        {
            if (EditorUtility.DisplayDialog("Clear the current Level ? ",
                "This will wipe any unsaved changes or progress in the scene.",
                "Clear", "Cancel"))
            {
                ClearLevel();
            }
        }

        // Level Utility Methods
        if (GUILayout.Button("New Level"))
        {
            NewLevel();
        }


        EditorGUILayout.Space(); // Space

        if (_currentSavedLevelName == null)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level Name : ", GUILayout.MaxWidth(128));
            _levelName = EditorGUILayout.TextField(_levelName);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Save Level As"))
                SaveLevelAs();
        }
        else
        {
            EditorGUILayout.LabelField("Current Level : " + _currentSavedLevelName, EditorStyles.boldLabel);
            if (GUILayout.Button("Save Current Level"))
                SaveCurrentLevel();
        }

        
        EditorGUILayout.Space(); // Space

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level", GUILayout.MaxWidth(128));
        _levelFile = EditorGUILayout.ObjectField(_levelFile, typeof(TextAsset), false) as TextAsset;
        GUILayout.EndHorizontal();

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
        
        if(LevelEditorGrid == null) Close();
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
        }
    }

    private void PlaceCube(RaycastHit hit)
    {
        // get hit cube
        var hitCube = hit.collider.GetComponent<CubeObject_LevelEditor>();

        // deduce where it is you want to place the cube
        Vector3 newIndex = hitCube.Coords.Pos[0] + hit.normal;
        TriCoords cubeCoords = new TriCoords((int) newIndex.x, (int) newIndex.y, (int) newIndex.z);

        //if (LevelEditorGrid.Nodes[hitCube.]) ;

        // spawn the new cube
        GameObject prefabCube = PoolManager.PlaceCube((CubeBehaviors)_placingCubeType);
        var newCube = prefabCube.GetComponent<CubeObject_LevelEditor>();
        
        // set cube type and position
        newCube.ConfigCube(cubeCoords, _placingCubeType);
        prefabCube.transform.position = hit.transform.position + hit.normal * AbstractGrid.width;
        prefabCube.transform.parent = GridParentObj;
        
        // keep ref to cube
        LevelEditorGrid.placedCubes.Add(prefabCube.GetComponent<AbstractCubeObject>());

        UpdateGrid();
        
        Undo.RegisterCreatedObjectUndo(prefabCube, "Undo New Cube");
        _event.Use();
    }

    private void RemoveCube(RaycastHit hit)
    {
        GameObject destroyed = hit.transform.gameObject;

        LevelEditorGrid.placedCubes.Remove(destroyed.GetComponent<AbstractCubeObject>());

        Undo.DestroyObjectImmediate(destroyed);

        DestroyImmediate(destroyed);

        UpdateGrid();

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

            actionMenu.AddItem(new GUIContent("None"), _rightIsNone,
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

            cubeMenu.AddItem(new GUIContent("None"), _leftIsNone,
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
                () => { RefreshMenu_Right(ComplexCubeType.HeavyVictory); });

            cubeMenu.AddItem(new GUIContent("Mine"), _isMine,
                () => { RefreshMenu_Right(ComplexCubeType.Mine); });

            cubeMenu.AddItem(new GUIContent("Victory Mine"), _isVictoryMine,
                () => { RefreshMenu_Right(ComplexCubeType.MineVictory); });

            cubeMenu.AddItem(new GUIContent("Counter"), _isCounter,
                () => { RefreshMenu_Right(ComplexCubeType.Counter); });

            cubeMenu.AddItem(new GUIContent("Switcher"), _isSwitcher,
                () => { RefreshMenu_Right(ComplexCubeType.Switcher); });

            cubeMenu.AddItem(new GUIContent("Victory Switcher"), _isVictorySwitcher,
                () => { RefreshMenu_Right(ComplexCubeType.SwitcherVictory); });

            cubeMenu.AddItem(new GUIContent("Rotator"), _isRotator,
                () => { RefreshMenu_Right(ComplexCubeType.Rotator); });

            cubeMenu.ShowAsContext();

            Repaint();

            _event.Use();
        }
    }

    void RefreshMenu_Left(EditorAction action)
    {
        _editorAction = action;
        _rightIsNone = _editorAction == EditorAction.None;
        _isPlacing = _editorAction == EditorAction.Place;
        _isRemoving = _editorAction == EditorAction.Remove;
        _isRotating = _editorAction == EditorAction.Rotate;
    }

    void RefreshMenu_Right(ComplexCubeType complexCubeType)
    {
        _placingCubeType = complexCubeType;
        _leftIsNone = complexCubeType == ComplexCubeType.None;
        _isStatic = complexCubeType == ComplexCubeType.Static;
        _isMoveable = complexCubeType == ComplexCubeType.Moveable;
        _isVictory = complexCubeType == ComplexCubeType.MoveableVictory;
        _isDelivery = complexCubeType == ComplexCubeType.StaticDelivery;
        _isElevator = complexCubeType == ComplexCubeType.StaticElevator;
        _isHeavy = complexCubeType == ComplexCubeType.Heavy;
        _isVictoryHeavy = complexCubeType == ComplexCubeType.HeavyVictory;
        _isMine = complexCubeType == ComplexCubeType.Mine;
        _isVictoryMine = complexCubeType == ComplexCubeType.MineVictory;
        _isCounter = complexCubeType == ComplexCubeType.Counter;
        _isSwitcher = complexCubeType == ComplexCubeType.Switcher;
        _isVictorySwitcher = complexCubeType == ComplexCubeType.SwitcherVictory;
        _isRotator = complexCubeType == ComplexCubeType.Rotator;
    }

    #endregion

    #region Utility Functions

    private void ClearLevel()
    {
        LevelEditorGrid.ClearNodes();
        _currentSavedLevelName = null;
    }

    // Called when the user wants to start building a new level
    private void NewLevel()
    {
        if (EditorUtility.DisplayDialog("Create a new Level ? ",
            "This will wipe any unsaved changes or progress in the scene.",
            "Create", "Cancel"))
        {
            LevelEditorGrid.ClearNodes();

            GameObject newObject = PoolManager.PlaceCube((CubeBehaviors)_startingCubeType);

            var newCube = newObject.GetComponent<CubeObject_LevelEditor>();

            // set cube type and data
            newCube.ConfigCube(TriCoords.Zero, _startingCubeType);

            LevelEditorGrid.ClearNodes();
            LevelEditorGrid.placedCubes.Add(newObject.GetComponent<AbstractCubeObject>());

            newObject.transform.position = Vector3.zero;
            newObject.transform.parent = GridParentObj;

            _currentSavedLevelName = null;
        }
    }
    
    // Called by user to save the level as a new file
    private void SaveLevelAs()
    {
        LevelEditorGrid.GenerateNodes();
        LevelSaver.CreateNewSave(LevelEditorGrid.Nodes, _levelName);
        _currentSavedLevelName = _levelName;
    }

    private void SaveCurrentLevel()
    {
        LevelEditorGrid.GenerateNodes();
        LevelSaver.CreateNewSave(LevelEditorGrid.Nodes, _currentSavedLevelName);
    }

    private void OpenLevel()
    {
        ClearLevel();
        LevelLoader.OpenLevel(AssetDatabase.GetAssetPath(_levelFile));

        // I realize that this is absolutely stupid
        string json = File.ReadAllText(AssetDatabase.GetAssetPath(_levelFile));
        var thing = JsonUtility.FromJson<SaveFile>(json);
        _currentSavedLevelName = thing.Name;
    }


    private void UpdateGrid()
    {
        int sizeX = 0, sizeY = 0, sizeZ = 0;

        for (int i = 0; i < LevelEditorGrid.placedCubes.Count; i++)
        {
            // 
            if (LevelEditorGrid.placedCubes[i])
            {
                sizeX = sizeX < LevelEditorGrid.placedCubes[i].Coords.Pos[0].x
                    ? (int) LevelEditorGrid.placedCubes[i].Coords.Pos[0].x
                    : sizeX;
                sizeY = sizeY < LevelEditorGrid.placedCubes[i].Coords.Pos[0].y
                    ? (int) LevelEditorGrid.placedCubes[i].Coords.Pos[0].y
                    : sizeY;
                sizeZ = sizeZ < LevelEditorGrid.placedCubes[i].Coords.Pos[0].z
                    ? (int) LevelEditorGrid.placedCubes[i].Coords.Pos[0].z
                    : sizeZ;
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