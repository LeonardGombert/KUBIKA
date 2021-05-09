using System.IO;
using UnityEngine;

public class LevelLoader_Editor : AbstractLevelLoader
{
    public override void OpenLevel(string path)
    {
        string json = File.ReadAllText(path);
        
        _levelFile = JsonUtility.FromJson<SaveFile>(json);

        FindObjectOfType<CubePoolManager_LevelEditor>().AssembleLevel(_levelFile.Nodes);
        FindObjectOfType<Grid_LevelEditor>().GridEquals(_levelFile.Nodes);
    }
}