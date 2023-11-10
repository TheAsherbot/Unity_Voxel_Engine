using UnityEngine;

namespace TheAshBot.VoxelEngine
{
    public struct VoxelNode
    {

        public VoxelNode(GenericGrid3D<VoxelNode> grid, byte x, byte y, byte z)
        {
            this.grid = grid;
            color = new Color32(0, 0, 0, 0);
            isFilled = false;
            this.x = x;
            this.y = y;
            this.z = z;

            neighbors = new VoxelNode[0];
            UpdateNeighbors();
        }


        public bool isFilled;
        public byte x;
        public byte y;
        public byte z;
        public Color32 color;
        private GenericGrid3D<VoxelNode> grid;

        /// <summary>
        /// Is the 6 direct neighbors. 0 = Top, 1 = Bottom, 2 = Front, 3 = Back, 4 = Right, 5 = Left
        /// </summary>
        public VoxelNode[] neighbors;




        public void UpdateNeighbors()
        {
            neighbors = new VoxelNode[]
            {
                grid.GetGridObject(x, y + 1, z),
                grid.GetGridObject(x, y - 1, z),
                grid.GetGridObject(x, y, z + 1),
                grid.GetGridObject(x, y, z - 1),
                grid.GetGridObject(x + 1, y, z),
                grid.GetGridObject(x - 1, y, z),
            };
        }

        public override string ToString()
        {
            return (isFilled == false ? "" : "Filled");
        }

    }
}
