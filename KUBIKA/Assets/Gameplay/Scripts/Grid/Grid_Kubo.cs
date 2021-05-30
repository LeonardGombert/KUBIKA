using Sirenix.OdinInspector;
using UnityEngine;

public class Grid_Kubo : AbstractGrid
{
    [ShowInInspector, ReadOnly, TableList] public static KuboState State;
    [SerializeField] private int sizeX, sizeY, sizeZ;

   /*private void Awake() => GenerateNodes();

    public override void GenerateNodes()
    {
        Nodes = new Node[sizeX * sizeY * sizeZ];

        for (int z = 0, i = 0; z < sizeZ; z++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++, i++)
                {
                    //Grid[i] = new KuboNode(x, y, z, ComplexCubeType.None);
                }
            }
        }
    }*/
   
   
   
   public override void GenerateNodes()
   {
       throw new System.NotImplementedException();
   }
}