using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class UndoManager : MonoBehaviour
    {
        [SerializeField] private BehaviorManager_Movement movementManager;
        private Stack<List<CubeBehavior_Movement>> thingsToUndo = new Stack<List<CubeBehavior_Movement>>();

        private void Start()
        {
            movementManager.doneMovingCubes += RegisterOneMove;
        }

        private void RegisterOneMove(List<CubeBehavior_Movement> obj)
        {
            thingsToUndo.Push(obj);
        }

        public void Undo()
        {
            for (int i = 0; i < thingsToUndo.Peek().Count; i++)
            {
                thingsToUndo.Peek()[i].UndoLast();
            }

            thingsToUndo.Pop();
        }
    }
}