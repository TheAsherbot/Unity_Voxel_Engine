using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelNode
{

    public VoxelNode(VoxelGrid<VoxelNode> grid, int x, int y, int z)
    {
        type = new Bit[2];
        type[0] = 0;
        type[1] = 0;
        this.x = x; 
        this.y = y; 
        this.z = z;
    }

    /// <summary>
    /// 0 = Empty, 1 = Filled, 2 = almost full.
    /// </summary>
    public Bit[] type;
    public float x; 
    public float y; 
    public float z;


    public override string ToString()
    {
        int typeInt = type[0] + type[1];
        return typeInt == 0 ? "" : typeInt == 1 ? "Filled" : "Almost Full";
    }

}
