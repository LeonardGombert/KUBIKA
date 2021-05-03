using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class KuboLevelEditorWindow : OdinEditorWindow
{
    LevelEditor_KuboGrid _kuboGrid;
    [HideInInspector] public EditorAction editorAction;
    [HideInInspector] public CubeType cubeType;
    private GameObject _cubeToPlace;
    private Event _event;
    private bool _isNone = true, _isPlacing, _isRemoving, _isRotating;

    public static void OpenWindow(LevelEditor_KuboGrid kuboGrid)
    {
        var window = GetWindow<KuboLevelEditorWindow>();
        window.Show();
        window._kuboGrid = kuboGrid;
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
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Editor Action");
        editorAction = (EditorAction) EditorGUILayout.EnumPopup(editorAction);
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Placing Cube");
        cubeType = (CubeType) EditorGUILayout.EnumPopup(cubeType);
        GUILayout.EndHorizontal();
        
        _cubeToPlace = (GameObject) EditorGUILayout.ObjectField(_cubeToPlace, typeof(GameObject), true);

        GUILayout.EndVertical();
    }

    private void CustomUpdate(SceneView sceneView)
    {
        _event = Event.current;
        RegisterActions();
        ContextMenu();
    }

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

    #region Editor Actions

    private void PlaceCube(RaycastHit hit)
    {
        GameObject newObject = (GameObject) PrefabUtility.InstantiatePrefab(_cubeToPlace);
        
        var newCube = newObject.GetComponent<Cube_LevelEditor>();
        var hitCube = hit.collider.GetComponent<Cube_LevelEditor>();
        
        Vector3 newIndex = hitCube.Index.Config[0] + hit.normal;
        
        newCube.Index = new KuboVector((int)newIndex.x, (int)newIndex.y, (int)newIndex.z);
        newObject.transform.position = hit.transform.position + hit.normal * 1.1f;

        Undo.RegisterCreatedObjectUndo(newObject, "Undo New Cube");
        
        _event.Use();
    }

    private void Remove(RaycastHit hit)
    {
        GameObject destroyed = hit.transform.gameObject;

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
}