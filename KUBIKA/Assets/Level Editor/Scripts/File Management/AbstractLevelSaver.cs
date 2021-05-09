using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractLevelSaver : MonoBehaviour
{
    [SerializeField, ReadOnly] protected string saveFilePath;
    [SerializeField, ReadOnly] protected string fullPath;
    [SerializeField] protected SaveFile _savefile;

    public abstract bool CreateNewSave(GridNode[] nodes, string levelName);
}
