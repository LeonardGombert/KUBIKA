using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelLoader_Game : AbstractLevelLoader
{
    [SerializeField] private Grid_Kubo gameGrid;
    [SerializeField] private CubePoolManager_Game cubePoolManager;
    // [SerializeField] protected Dictionary<CubeBehaviors, AbstractBehaviorManager> BehaviorManagers = new Dictionary<CubeBehaviors, AbstractBehaviorManager>();
    
    [Button]
    public override void OpenLevel(string path = "Assets/_MASTER/Resources/Levels/Transportation.json")
    {
        string json = File.ReadAllText(path);
        
        _levelFile = JsonUtility.FromJson<SaveFile>(json);

        cubePoolManager.AssembleLevel(_levelFile.Nodes);
        gameGrid.NodesToDictionary(_levelFile.Nodes);
    }
}
