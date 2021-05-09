using System.IO;
using UnityEngine;

public class LevelLoader_Game : AbstractLevelLoader
{
    [SerializeField] private Grid_Kubo cubeGrid;
    [SerializeField] private CubePoolManager_Game cubePoolManager;

    public override void OpenLevel(string path)
    {
        string json = File.ReadAllText(path);
        
        _levelFile = JsonUtility.FromJson<SaveFile>(json);

        cubePoolManager.AssembleLevel(_levelFile.Nodes);
        cubeGrid.GridEquals(_levelFile.Nodes);
    }
}
