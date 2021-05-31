using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public struct SaveFile
{
    // [SerializeField] private string _kubicode;
    // [SerializeField] private Biome _biome;
    // [SerializeField] private int _minimumMoves;
    // [SerializeField] private bool _lockRotate;

    [SerializeField] private string _levelName;
    [SerializeField] private Node[] _nodes;

    public SaveFile(Node[] nodes, string levelName)
    {
        _nodes = nodes;
        _levelName = levelName;
    }

    /// <summary>
    /// Returns the stored name of the Level.
    /// </summary>
    public string Name => _levelName;
    /// <summary>
    /// Returns the array of Nodes that makes up the level's grid.
    /// </summary>
    public Node[] Nodes => _nodes;
}