using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Kubika.Testing
{
    using UnityEngine;

    public class GenerateCubes : SerializedMonoBehaviour
    {
        public CubeBehaviors cubeType;
        [SerializeField] Dictionary<CubeBehaviors, AbstractCubeFactory> _cubeFactories = new Dictionary<CubeBehaviors, AbstractCubeFactory>();

        void Update()
        {
         //   if (Input.GetKeyDown(KeyCode.Space)) _cubeFactories[cubeType].PlaceCube(Vector3.zero);
        }
    }
}