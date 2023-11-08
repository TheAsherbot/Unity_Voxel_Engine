using UnityEngine;

namespace TheAshBot.VoxelEngine
{
    public class VoxelChunk
    {

        private static readonly byte GRID_SIZE = 16;




        /// <summary>
        /// 0 = empty, 1 = partially filled, 2 = filled.
        /// </summary>
        private BitArray chunkStatus;

        private GenericGrid3D<VoxelNode> grid;




        public VoxelChunk(float cellSize, Vector3 originPosition)
        {
            grid = new GenericGrid3D<VoxelNode>(GRID_SIZE, GRID_SIZE, GRID_SIZE, cellSize, originPosition, 
                (GenericGrid3D<VoxelNode> grid, byte x, byte y, byte z) => new VoxelNode(grid, x, y, z));

            chunkStatus = new BitArray(2);
        }
        public VoxelChunk(GenericGrid3D<VoxelChunk> chunkGrid, int x, int y, int z)
        {
            grid = new GenericGrid3D<VoxelNode>(GRID_SIZE, GRID_SIZE, GRID_SIZE, 1, new Vector3(x, y, z) * 16,
                (GenericGrid3D<VoxelNode> grid, byte x, byte y, byte z) => new VoxelNode(grid, x, y, z));

            chunkStatus = new BitArray(2);
        }





        /// <summary>
        /// Gets the chunk Status of the chunk. 0 = empty, 1 = partially filled, 2 = filled.
        /// </summary>
        /// <returns>A bit array with 2 bits</returns>
        public BitArray GetChunkStatus()
        {
            return chunkStatus;
        }
        public GenericGrid3D<VoxelNode> GetGrid()
        {
            return grid;
        }

    }
}
