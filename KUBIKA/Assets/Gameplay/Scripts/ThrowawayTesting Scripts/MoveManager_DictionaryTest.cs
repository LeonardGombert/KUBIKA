using System.Collections.Generic;
using Sirenix.OdinInspector;

public class MoveManager_DictionaryTest : SerializedMonoBehaviour
{
    [ShowInInspector] Dictionary<Vector3Kubo, CubeBehavior_Movement> movementCubes = new Dictionary<Vector3Kubo, CubeBehavior_Movement>();
    public CubeBehaviour_Base cubes;
    public CubeBehaviour_Base cubes2;
    private void Awake()
    {
       //movementCubes.Add(cubes.gridPosition, cubes.GetComponent<CubeBehavior_Movement>());
       //movementCubes.Add(cubes2.gridPosition, cubes2.GetComponent<CubeBehavior_Movement>());
    }
}
