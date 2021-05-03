using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public struct KuboVector
{
    [ShowInInspector] public Vector3[] Config { get; private set; }

    public KuboVector(int x, int y, int z)
    {
        Config = new Vector3[3];
        Config[0] = new Vector3(x, y, z); // position 1
        Config[1] = new Vector3(z, x, y); // position 2
        Config[2] = new Vector3(y, z, x); // position 3
    }
}