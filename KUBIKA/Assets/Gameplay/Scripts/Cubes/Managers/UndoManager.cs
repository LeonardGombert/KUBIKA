using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class UndoManager : MonoBehaviour
    {
        [SerializeField] private BehaviorManager_Movement movementManager;
        private Stack<Dictionary<CubeBehavior_Movement, Node>> cubesToUndo = new Stack<Dictionary<CubeBehavior_Movement, Node> >();

        public void RegisterOneMove(Dictionary<CubeBehavior_Movement, Node> list)
        {
            cubesToUndo.Push(list);
        }

        public void Undo()
        {
            foreach (var cube in cubesToUndo.Peek())
            {
                cube.Key.MoveCubeToNode(cube.Value);
            }

            cubesToUndo.Pop();
        }
    }
}