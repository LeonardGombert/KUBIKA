using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Kubika.Testing
{
    using UnityEngine;

    public class GenerateCubes : SerializedMonoBehaviour
    {
        public CubeType cubeType;
        [SerializeField] Dictionary<CubeType, Abstract_CubeFactory> _cubeFactories = new Dictionary<CubeType, Abstract_CubeFactory>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _cubeFactories[cubeType].PlaceCube(KuboVector.Zero);
        }
    }
}