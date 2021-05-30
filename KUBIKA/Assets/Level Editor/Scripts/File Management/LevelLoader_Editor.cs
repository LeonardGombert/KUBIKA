using System.IO;
using UnityEngine;

public class LevelLoader_Editor : AbstractLevelLoader
{
    [SerializeField] private CubePoolManager_LevelEditor cubePoolManager;
    [SerializeField] private Grid_LevelEditor levelEditor;
    
    public override void OpenLevel(string path)
    {
        string json = File.ReadAllText(path);
        
        _levelFile = JsonUtility.FromJson<SaveFile>(json);

        cubePoolManager.AssembleLevel(_levelFile.Nodes);
        levelEditor.AssignLoadedGrid(_levelFile.Nodes);
    }
}