using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Gameplay.Scripts.Cubes.Managers
{
    public abstract class AbstractBehaviorManager<T> : SerializedMonoBehaviour
    {
        /// <summary>
        /// BehaviorManagers have a single dictionary that references all behaviors of its type. They
        /// are indexed by their coordinates, and automatically updated.
        /// </summary>
        protected Dictionary<TriCoords, T> CubeBehaviorsDictionary =
            new Dictionary<TriCoords, T>();

        /// <summary>
        /// Used to add all of the cubes on a given type to the dictionary. Example : when a level is loaded,
        /// all Mine Victory Cubes will add themselves to the MineBehaviourManager, VictoryBehaviourManager *
        /// and MoveableBehaviourManager dictionaries.
        /// </summary>
        /// <param name="coordinates">The current position of the Managed Cube. Is updated as the cube moves.</param>
        /// <param name="behavior">The Behaviour Type of the Cube. Serves as a reference to access its functionality.</param>
        public virtual void AddBehaviour(TriCoords coordinates, T behavior) => CubeBehaviorsDictionary.Add(coordinates, behavior);
    }
}