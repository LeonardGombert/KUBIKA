using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class UndoManager : MonoBehaviour
    {
        [SerializeField] private BehaviorManager_Movement movementManager;
        private Stack<List<IUndoable>> cubesToUndo = new Stack<List<IUndoable>>();
        private List<IUndoable> hotList = new List<IUndoable>();

        public void RegisterOne(IUndoable targetUndoable)
        {
            hotList.Add(targetUndoable);
        }
        
        public void RegisterOneMove()
        {
            var tempList = hotList;
            cubesToUndo.Push(tempList);
            hotList.Clear();
        }
        
        public void Undo()
        {
            for (int i = 0; i < cubesToUndo.Peek().Count; i++)
            {
                cubesToUndo.Peek()[i].UndoLast();
            }

            cubesToUndo.Pop();
        }
    }
}