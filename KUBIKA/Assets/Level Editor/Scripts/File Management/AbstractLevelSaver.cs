using UnityEngine;

public abstract class AbstractLevelSaver : MonoBehaviour
{
    [SerializeField] protected SaveFile savefile = new SaveFile();
}
