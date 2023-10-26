using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;

using UnityEngine;

public class Test : MonoBehaviour
{

    private Bit bit1;
    private Bit bit2;

    private void Start()
    {
        bit1 = true;
        bit2 = true;

        VoxelGrid<VoxelNode> grid = new VoxelGrid<VoxelNode>(10, 10, 10, 50, Vector3.zero, (VoxelGrid<VoxelNode> grid, int x, int y, int z) => new VoxelNode(grid, x, y, z), true, transform, 3);

        for (int i = 0; i < 10; i++)
        {
            VoxelNode voxel = grid.GetGridObject(i, 1 % 2, i % 3);
            voxel.type[0] = 1;
            grid.SetGridObject(i, 1 % 2, i % 3, voxel);
        }
    }

}
