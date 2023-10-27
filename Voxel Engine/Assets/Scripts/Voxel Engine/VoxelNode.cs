public class VoxelNode
{

    public VoxelNode(VoxelGrid<VoxelNode> grid, int x, int y, int z)
    {
        type = new BitArray(255);
        this.x = x; 
        this.y = y; 
        this.z = z;
    }

    /// <summary>
    /// 0 = Empty, 1 = Filled, 2 = almost full.
    /// </summary>
    public BitArray type;
    public float x; 
    public float y; 
    public float z;


    public override string ToString()
    {
        return "Filled";
    }

}
