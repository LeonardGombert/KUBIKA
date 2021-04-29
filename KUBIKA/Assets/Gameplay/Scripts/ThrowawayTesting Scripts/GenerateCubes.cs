using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Kubika.Testing
{
    using UnityEngine;

    public class GenerateCubes : SerializedMonoBehaviour
    {
        public CubeType cubeType;
        [SerializeField] Dictionary<CubeType, AbstractCubeFactory> _cubeFactories = new Dictionary<CubeType, AbstractCubeFactory>();

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _cubeFactories[cubeType].PlaceCube(0);
        }
    }
}