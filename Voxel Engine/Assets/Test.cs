using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;

using UnityEngine;

public class Test : MonoBehaviour
{

    private BitArray bitArray;

    private void Start()
    {
        bitArray = new BitArray(10);
        bitArray.SetBit(4, 1);

        Debug.Log(bitArray.GetValue_Int());

        VoxelGrid<VoxelNode> grid = new VoxelGrid<VoxelNode>(10, 10, 10, 50, Vector3.zero, (VoxelGrid<VoxelNode> grid, int x, int y, int z) => new VoxelNode(grid, x, y, z), true, transform, 3);

        for (int i = 0; i < 10; i++)
        {
            VoxelNode voxel = grid.GetGridObject(i, 1 % 2, i % 3);
            grid.SetGridObject(i, i % 2, i % 3, voxel);
        }
    }

}
