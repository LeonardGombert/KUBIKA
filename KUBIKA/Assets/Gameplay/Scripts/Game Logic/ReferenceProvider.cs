using System.Collections;
using System.Collections.Generic;
using Gameplay.Scripts.Cubes.Managers;
using UnityEngine;

public class ReferenceProvider : MonoBehaviour
{
    #region Singleton
    private static ReferenceProvider _instance;
    public static ReferenceProvider Instance => _instance;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(_instance);
    }
    #endregion

    [SerializeField] private Grid_Kubo kuboGrid;
    public Grid_Kubo KuboGrid => kuboGrid;
    
    [SerializeField] private BehaviourManager_PlayerInput playerInput;
    public BehaviourManager_PlayerInput PlayerInput => playerInput;
}
