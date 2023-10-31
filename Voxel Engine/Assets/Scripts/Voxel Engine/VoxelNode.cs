using UnityEngine;

public class VoxelNode
{

    public VoxelNode(GenericGrid3D<VoxelNode> grid, int x, int y, int z)
    {
        type = new BitArray(2);
        type.SetBit(0, 1);
        this.x = x; 
        this.y = y; 
        this.z = z;
    }

    /// <summary>
    /// 0 = Empty, 1 = Filled, 2 = Almost Filled.
    /// </summary>
    public BitArray type;
    public float x; 
    public float y; 
    public float z;
    public Color32 color;


    public override string ToString()
    {
        string value = "";
        if (type.GetValue_Byte() == 1)
        {
            value = "Filled";
        }
        if (type.GetValue_Byte() == 2)
        {
            value = "Almost Filled";
        }
        return value;
    }

}
