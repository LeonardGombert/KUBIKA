using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class UndoManager : MonoBehaviour
    {
        [SerializeField] private BehaviorManager_Movement movementManager;

        [ShowInInspector] private List<List<KeyValuePair<CubeBehavior_Movement, Node>>> thingsToUndo =
            new List<List<KeyValuePair<CubeBehavior_Movement, Node>>>();

        private void Start()
        {
            movementManager.doneMovingCubes += RegisterOneMove;
        }

        private void RegisterOneMove(List<KeyValuePair<CubeBehavior_Movement, Node>> obj)
        {
            thingsToUndo.Add(obj);
        }

        public void Undo()
        {
            for (int i = 0; i < thingsToUndo[0].Count; i++)
            {
                thingsToUndo[0][i].Key.transform.position = thingsToUndo[0][i].Value.worldPosition;
            }

            thingsToUndo.RemoveAt(0);
        }
    }
}