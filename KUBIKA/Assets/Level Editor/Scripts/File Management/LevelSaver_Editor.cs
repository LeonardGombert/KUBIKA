#if UNITY_EDITOR	
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelSaver_Editor : AbstractLevelSaver
{
    public override bool CreateNewSave(Node[,,] nodes, string levelName)
    {
        SaveFile saveFile = new SaveFile(nodes, levelName);
        
        string json = JsonUtility.ToJson(saveFile);
        
        saveFilePath = Application.dataPath + "/_MASTER/Resources/Levels";

        if (!Directory.Exists(saveFilePath))
        {
            Directory.CreateDirectory(saveFilePath);
        }

        fullPath = Path.Combine(saveFilePath, levelName) + ".json";

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        
        File.WriteAllText(fullPath, json);
        
        AssetDatabase.Refresh();

        return false;
    }
}

#endif