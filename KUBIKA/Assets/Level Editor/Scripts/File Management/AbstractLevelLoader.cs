using UnityEngine;

public abstract class AbstractLevelLoader : MonoBehaviour
{
    [SerializeField] protected SaveFile _levelFile;

    public abstract void OpenLevel(string path);
}
