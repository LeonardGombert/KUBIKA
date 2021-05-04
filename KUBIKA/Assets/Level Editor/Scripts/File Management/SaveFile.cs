using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public struct SaveFile
{
    [SerializeField] private string _levelName;

    // [SerializeField] private string _kubicode;
    // [SerializeField] private Biome _biome;
    // [SerializeField] private int _minimumMoves;
    // [SerializeField] private bool _lockRotate;
    
    [SerializeField] private GridNode[] _nodes;

    public SaveFile(GridNode[] nodes, string levelName)
    {
        _nodes = nodes;
        _levelName = levelName;
    }
}