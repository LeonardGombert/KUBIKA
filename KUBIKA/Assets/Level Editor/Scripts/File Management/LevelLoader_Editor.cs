using System.IO;
using UnityEngine;

public class LevelLoader_Editor : AbstractLevelLoader
{
    [SerializeField] private CubePoolManager_Game cubePoolManager;
    
    public override void OpenLevel(string path)
    {
        string json = File.ReadAllText(path);
        
        _levelFile = JsonUtility.FromJson<SaveFile>(json);

        FindObjectOfType<CubePoolManager_LevelEditor>().AssembleLevel(_levelFile.Nodes);
        FindObjectOfType<Grid_LevelEditor>().AssignLoadedGrid(_levelFile.Nodes);
    }
}