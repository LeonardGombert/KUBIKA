using System.IO;
using UnityEngine;

public class LevelLoader_Editor : AbstractLevelLoader
{
    private static CubePoolManager_LevelEditor CubePoolManager => FindObjectOfType<CubePoolManager_LevelEditor>();
    private static Grid_LevelEditor LevelEditor=> FindObjectOfType<Grid_LevelEditor>();
    
    public override void OpenLevel(string path)
    {
        string json = File.ReadAllText(path);
        
        _levelFile = JsonUtility.FromJson<SaveFile>(json);

        CubePoolManager.AssembleLevel(_levelFile.Nodes);
        LevelEditor.AssignLoadedGrid(_levelFile.Nodes);
    }
}