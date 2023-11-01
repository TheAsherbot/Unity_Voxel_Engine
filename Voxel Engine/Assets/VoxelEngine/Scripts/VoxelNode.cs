using UnityEngine;

namespace TheAshBot.VoxelEngine
{
    public class VoxelNode
    {

        public VoxelNode(GenericGrid3D<VoxelNode> grid, int x, int y, int z)
        {
            isEmpty = true;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// 0 = Empty, 1 = Filled, 2 = Almost Filled.
        /// </summary>
        public bool isEmpty;
        public float x;
        public float y;
        public float z;
        public Color32 color;


        public override string ToString()
        {
            string value = isEmpty ? "Empty" : "Filled";
            return value;
        }

    }
}
