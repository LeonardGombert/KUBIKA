[System.Serializable]
public struct KuboVector
{
    [UnityEngine.SerializeField] private int xPos, yPos, zPos;
    
    public KuboVector(int _xPos, int _yPos, int _zPos)
    {
        xPos = _xPos;
        yPos = _yPos;
        zPos = _zPos;
    }
}
