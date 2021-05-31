using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractLevelLoader : SerializedMonoBehaviour
{
    [SerializeField] protected SaveFile _levelFile;

    public abstract void OpenLevel(string path);
}
