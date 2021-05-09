using System.IO;
using UnityEngine;

public class LevelLoader_Editor : AbstractLevelLoader
{
    public override void OpenLevel(string path)
    {
        string json = File.ReadAllText(path);
        
        _levelFile = JsonUtility.FromJson<SaveFile>(json);

        FindObjectOfType<CubePoolManager>().AssembleLevel(_levelFile.Nodes);
    }
}