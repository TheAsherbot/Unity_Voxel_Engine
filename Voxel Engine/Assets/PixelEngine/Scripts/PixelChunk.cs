using TheAshBot.VoxelEngine;

using UnityEngine;

namespace TheAshBot.PixelEngine
{
    public class PixelChunk
    {

        private static readonly byte GRID_SIZE = 16;




        /// <summary>
        /// 0 = empty, 1 = partially filled, 2 = filled.
        /// </summary>
        private BitArray chunkStatus;

        private GenericGrid2D<PixelNode> grid;




        public PixelChunk(float cellSize, Vector3 originPosition)
        {
            grid = new GenericGrid2D<PixelNode>(GRID_SIZE, GRID_SIZE, cellSize, originPosition, 
                (GenericGrid2D<PixelNode> grid, byte x, byte y) => new PixelNode(grid, x, y));

            chunkStatus = new BitArray(2);
        }
        public PixelChunk(GenericGrid2D<PixelChunk> chunkGrid, int x, int y)
        {
            grid = new GenericGrid2D<PixelNode>(GRID_SIZE, GRID_SIZE, 1, new Vector2(x, y) * 16,
                (GenericGrid2D<PixelNode> grid, byte x, byte y) => new PixelNode(grid, x, y));

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
        public GenericGrid2D<PixelNode> GetGrid()
        {
            return grid;
        }

    }
}
