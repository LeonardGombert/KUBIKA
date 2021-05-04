using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Kubika.Testing
{
    using UnityEngine;

    public class GenerateCubes : SerializedMonoBehaviour
    {
        public CubeBehaviors cubeType;
        [SerializeField] Dictionary<CubeBehaviors, Abstract_CubeFactory> _cubeFactories = new Dictionary<CubeBehaviors, Abstract_CubeFactory>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _cubeFactories[cubeType].PlaceCube(GridCoord.Zero);
        }
    }
}